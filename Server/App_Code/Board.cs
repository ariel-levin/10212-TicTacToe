/*********************************  
 *  Ariel Levin
 *  ariel.lvn89@gmail.com
 *  http://about.me/ariel.levin
 *********************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


public class Board
{
    private ICallBack player1, player2;
    private TTT server;
    private Game game;
    private bool singlePlayer;
    private char[,] board;
    private bool waitingForPlayer2, gameEnded;
    private int dim, moveCount;
    private Random rand;


	public Board(int dim, bool singlePlayer, ICallBack player, TTT server)
	{
        this.dim = dim;
        this.singlePlayer = singlePlayer;
        this.server = server;
        initBoard();
        moveCount = 0;
        gameEnded = false;
        player1 = player;
        if (singlePlayer)
            rand = new Random();

        game = new Game();
        game.BoardSize = dim;
        game.Player1 = server.getPlayerData(player).Id;
        game.StartTime = DateTime.Now;
        //game.Moves = "{ \"moves\": [";        // json
        game.Moves = "";                        // simple array

        if (singlePlayer)
            game.Player2 = 1;
        
        waitingForPlayer2 = !singlePlayer;
	}

    private void initBoard()
    {
        board = new char[dim, dim];
        for (var i = 0; i < board.GetLength(0); i++)
            for (var j = 0; j < board.GetLength(1); j++)
                board[i, j] = ' ';
    }

    private char getToken(ICallBack player)
    {
        if (player == player1)
            return 'X';
        else if (player == player2)
            return 'O';
        else
            return 'E';     // error
    }

    private ICallBack getOpponent(ICallBack player)
    {
        if (singlePlayer)
            return null;

        if (player == player1)
            return player2;
        else if (player == player2)
            return player1;
        else
            return null;
    }

    private bool isPressed(int row, int col)
    {
        if (row < 0 || row >= board.GetLength(0) || col < 0 || col >= board.GetLength(1))
            return true;
        else
            return board[row, col] != ' ';
    }

    private bool isBoardFull()
    {
        return moveCount >= dim * dim;
    }

    private bool checkWinning(char token)
    {
        int i, j;

        // check rows
        for (i = 0; i < dim; i++)
        {
            j = 0;
            while (j < dim && board[i, j] == token)
                j++;

            if (j == dim)
                return true;
        }

        // check cols
        for (j = 0; j < dim; j++)
        {
            i = 0;
            while (i < dim && board[i, j] == token)
                i++;

            if (i == dim)
                return true;
        }

        // check major diagonal
        i = 0; j = 0;
        while (i < dim && board[i, j] == token)
        {
            i++; j++;
        }

        if (i == dim)
            return true;
            
        // check sub-diagonal
        i = dim-1; j = 0;
        while (j < dim && board[i, j] == token)
        {
            i--; j++;
        }

        if (j == dim)
            return true;

        return false;
    }

    private void computerMove(ICallBack player)
    {
        Thread.Sleep(1000);

        int row = -1, col = -1;

        do
        {
            row = rand.Next(0, dim);
            col = rand.Next(0, dim);

        } while (isPressed(row, col));

        board[row, col] = 'O';
        addGameMove(row, col);
        moveCount++;
        player.opponentPressed(row, col);

        if (checkWinning('O'))
        {
            player.gameEnded("You lose..");
            game.Winner = 1;
            endGame();
        }
        else if (isBoardFull())
        {
            player.gameEnded("It's a tie..");
            endGame();
        }
        else
            player.playerTurn();
    }

    private void addGameMove(int row, int col)
    {
        // json
        //if (moveCount > 0)
        //    game.Moves += ",";
        //game.Moves += " { \"r\": " + row + ", \"c\": " + col + " }";

        // simple array
        if (moveCount > 0)
            game.Moves += ":";
        game.Moves += row + "," + col;
    }

    private void endGame()
    {
        gameEnded = true;
        //game.Moves += " ] }";     // json
        if (player2 != null || singlePlayer)
        {
            game.EndTime = DateTime.Now;
            server.insertGameToDB(game);
        }
    }

    public bool isWaitingForPlayer2()
    {
        return waitingForPlayer2;
    }

    public bool isGameEnded()
    {
        return gameEnded;
    }

    public int getDimension()
    {
        return dim;
    }

    public void startMultiplayerGame()
    {
        player1.startGame(true);
        player2.startGame(false);
    }

    public void addPlayer(ICallBack player, int dim)
    {
        if (this.dim != dim)
        {
            player.gameError("Something went wrong...\nDifferenet dimensions of board");
            return;
        }

        if (player1 == null)
        {
            player.gameError("Something went wrong...\nPlayer1 is not found");
            return;
        }

        if (player == player1)
        {
            player.gameError("Something went wrong...\nTrying to add the same player");
            return;
        }

        if (player1 != null && singlePlayer)
        {
            player.gameError("Something went wrong...\nThe game is already occupied by single player game");
            return;
        }

        waitingForPlayer2 = false;
        player2 = player;
        game.Player2 = server.getPlayerData(player).Id;
    }

    public void playerPressed(ICallBack player, int row, int col)
    {
        char token = getToken(player);
        if (token == 'E')
        {
            player.gameError("Something went wrong...\nPlayer is not found");
            return;
        }

        if (isPressed(row, col))
        {
            player.gameError("Something went wrong...\nCell already pressed");
            return;
        }

        board[row, col] = token;
        addGameMove(row, col);
        moveCount++;

        ICallBack opponent = getOpponent(player);   // if singleplayer it will be null

        if (!singlePlayer)
        {
            if (opponent != null)
                opponent.opponentPressed(row, col);
            else
            {
                player.gameError("Something went wrong...\nOpponent is not found");
                return;
            }
        }

        if (checkWinning(token))
        {
            player.gameEnded("You won!");
            if (!singlePlayer && opponent != null)
                opponent.gameEnded("You lose..");
            game.Winner = server.getPlayerData(player).Id;
            endGame();
        }
        else if (isBoardFull())
        {
            player.gameEnded("It's a tie..");
            if (!singlePlayer && opponent != null)
                opponent.gameEnded("It's a tie..");
            endGame();
        }
        else
        {
            if (singlePlayer)
            {
                Task t = Task.Run(() => { computerMove(player); });
            }
            else
            {
                if (opponent != null)
                    opponent.playerTurn();
            }
        }

    }

    public void playerExit(ICallBack player)
    {
        if (!gameEnded)
        {
            if (!singlePlayer)
            {
                ICallBack op = getOpponent(player);
                if (op != null)
                    op.gameEnded("Opponent left the game..");
            }
            endGame();
        }
    }

}