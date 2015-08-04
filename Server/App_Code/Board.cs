using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


public class Board
{
    private ICallBack player1, player2;
    private bool singlePlayer;
    private char[,] board;
    private bool waitingForPlayer2;
    private bool gameEnded;
    private int dim;
    private int moveCount;
    private Random rand;


	public Board(int dim, bool singlePlayer, ICallBack player)
	{
        this.dim = dim;
        this.singlePlayer = singlePlayer;
        initBoard();
        moveCount = 0;
        gameEnded = false;
        rand = new Random();
        player1 = player;
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
        moveCount++;
        player.opponentPressed(row, col, dim);

        if (checkWinning('O'))
        {
            player.gameEnded("You lose..", dim);
            gameEnded = true;
        }
        else if (isBoardFull())
        {
            player.gameEnded("It's a tie..", dim);
            gameEnded = true;
        }
        else
            player.playerTurn(dim);
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
        player1.startGame(true, dim);
        player2.startGame(false, dim);
    }

    public void addPlayer(ICallBack player, int dim)
    {
        if (this.dim != dim)
        {
            player.gameError("Something went wrong...\nDifferenet dimensions of board", dim);
            return;
        }

        if (player1 == null)
        {
            player.gameError("Something went wrong...\nPlayer1 is not found", dim);
            return;
        }

        if (player == player1)
        {
            player.gameError("Something went wrong...\nTrying to add the same player", dim);
            return;
        }

        if (player1 != null && singlePlayer)
        {
            player.gameError("Something went wrong...\nThe game is already occupied by single player game", dim);
            return;
        }

        waitingForPlayer2 = false;
        player2 = player;
    }

    public void playerPressed(ICallBack player, int row, int col)
    {
        char token = getToken(player);
        if (token == 'E')
        {
            player.gameError("Something went wrong...\nPlayer is not found", dim);
            return;
        }

        if (isPressed(row, col))
        {
            player.gameError("Something went wrong...\nCell already pressed", dim);
            return;
        }

        board[row, col] = token;
        moveCount++;

        ICallBack opponent = getOpponent(player);   // if singleplayer it will be null

        if (!singlePlayer)
        {
            if (opponent != null)
                opponent.opponentPressed(row, col, dim);
            else
            {
                player.gameError("Something went wrong...\nOpponent is not found", dim);
                return;
            }
        }

        if (checkWinning(token))
        {
            player.gameEnded("You won!", dim);
            if (!singlePlayer)
            {
                if (opponent != null)
                    opponent.gameEnded("You lose..", dim);
            }
            gameEnded = true;
        }
        else if (isBoardFull())
        {
            player.gameEnded("It's a tie..", dim);
            if (!singlePlayer)
            {
                if (opponent != null)
                    opponent.gameEnded("It's a tie..", dim);
            }
            gameEnded = true;
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
                    opponent.playerTurn(dim);
            }
        }

    }

    public void playerExit(ICallBack player)
    {
        if (!singlePlayer)
        {
            ICallBack op = getOpponent(player);
            if (op != null)
                op.gameEnded("Opponent left the game..", dim);
        }
        gameEnded = true;
    }

}