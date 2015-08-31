/*********************************  
 *  Ariel Levin
 *  ariel.lvn89@gmail.com
 *  http://about.me/ariel.levin
 *********************************/

using Client.TTTService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
              
        
namespace Client
{
    public partial class BoardForm : Form
    {

        private MainForm mainForm;
        private int dim;
        private bool ended;
        private bool singlePlayer;
        private bool historyMode;
        private GameData gameHistory;
        private int[,] moves;
        private int currentMove;

        #region Boards
        private Board3Control ctrl3;
        private Board4Control ctrl4;
        private Board5Control ctrl5;
        #endregion


        // normal game constructor
        public BoardForm(int dim, MainForm mainForm, bool singlePlayer)
        {
            InitializeComponent();

            switch (dim)
            {
                case 3:
                    ctrl3 = new Board3Control(this);
                    boardElementHost.Child = ctrl3;
                    break;
                case 4:
                    ctrl4 = new Board4Control(this);
                    boardElementHost.Child = ctrl4;
                    break;
                case 5:
                    ctrl5 = new Board5Control(this);
                    boardElementHost.Child = ctrl5;
                    break;
            }
            
            this.dim = dim;
            this.mainForm = mainForm;
            this.singlePlayer = singlePlayer;
            this.Text = "Board " + dim + "x" + dim;
            this.historyMode = false;
            ended = false;
            showMessage("Waiting for server..");
            mainForm.getClient().startGameRequest(dim, singlePlayer);
        }

        // game history display constructor
        public BoardForm(GameData game, MainForm mainForm)
        {
            InitializeComponent();
            
            switch (game.BoardSize)
            {
                case 3:
                    ctrl3 = new Board3Control(this);
                    boardElementHost.Child = ctrl3;
                    ctrl3.disableBoard();
                    break;
                case 4:
                    ctrl4 = new Board4Control(this);
                    boardElementHost.Child = ctrl4;
                    ctrl4.disableBoard();
                    break;
                case 5:
                    ctrl5 = new Board5Control(this);
                    boardElementHost.Child = ctrl5;
                    ctrl5.disableBoard();
                    break;
            }

            this.dim = game.BoardSize;
            this.mainForm = mainForm;
            this.Text = "Board " + dim + "x" + dim;
            ended = true;
            this.gameHistory = game;
            this.historyMode = true;
            btnNext.Visible = true;

            string str = string.Format("{0}  vs.  {1}",
                            "[" + gameHistory.Player1_Name.Replace(" ", string.Empty) + "]",
                            "[" + gameHistory.Player2_Name.Replace(" ", string.Empty) + "]" );

            showMessage(str);

            this.moves = getGameMovesFromString(gameHistory.Moves);     // simple string
            if (moves != null && moves.Length > 0)
            {
                btnNext.Enabled = true;
                currentMove = 0;
            }
            else
                MessageBox.Show("No moves available for this game", "Moves", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void BoardForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
        }

        private void Board4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!historyMode)
            {
                try
                {
                    mainForm.getClient().playerExitGame();
                }
                catch (Exception) { }
                mainForm.boardForm = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!historyMode)
            {
                try
                {
                    mainForm.getClient().playerExitGame();
                }
                catch (Exception) { }
                mainForm.boardForm = null;
            }
            Dispose();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int row = moves[currentMove, 0];
            int col = moves[currentMove, 1];
            char currentToken = (currentMove % 2 == 0) ? 'X' : 'O';
            switch (gameHistory.BoardSize)
            {
                case 3:
                    ctrl3.showAnimation(row, col, currentToken);
                    break;
                case 4:
                    ctrl4.showAnimation(row, col, currentToken);
                    break;
                case 5:
                    ctrl5.showAnimation(row, col, currentToken);
                    break;
            }
            currentMove++;
            if (currentMove >= moves.GetLength(0))
            {
                btnNext.Enabled = false;
                MessageBox.Show("End of game", "Moves", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int[,] getGameMovesFromString(string str)
        {
            if (str.Length == 0)
                return null;

            try
            {
                string[] movesArr = str.Split(':');
                int[,] arr = new int[movesArr.Length, 2];
                
                for (var i = 0; i < movesArr.Length; i++)
                {
                    string[] move = movesArr[i].Split(',');
                    arr[i, 0] = int.Parse(move[0]);
                    arr[i, 1] = int.Parse(move[1]);
                }

                return arr;
            }
            catch (Exception)
            {
                showError("Some error occurred while getting game moves from DB");
                return null;
            }            
        }


        #region Public Methods

        public void setStatus(string status)
        {
            lblStatus.Text = status;
        }

        public TTTClient getClient()
        {
            return mainForm.getClient();
        }

        public void opponentPressed(int row, int col)
        {
            switch (dim)
            {
                case 3:
                    ctrl3.opponentPressed(row, col);
                    break;
                case 4:
                    ctrl4.opponentPressed(row, col);
                    break;
                case 5:
                    ctrl5.opponentPressed(row, col);
                    break;
            }
        }

        public void startGame(bool yourTurn)
        {
            if (yourTurn)
            {
                showMessage("Game on! Your turn");
                playerTurn();
            }
            else
                showMessage("Game on! Opponent's turn");
        }

        public void showError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void showMessage(string msg)
        {
            setStatus(msg);
        }

        public void addedSuccessfully(bool firstPlayer)
        {
            if (!singlePlayer && firstPlayer)
                showMessage("Waiting for a second player to join");

            switch (dim)
            {
                case 3:
                    ctrl3.setTokens((firstPlayer) ? 'X' : 'O');
                    break;
                case 4:
                    ctrl4.setTokens((firstPlayer) ? 'X' : 'O');
                    break;
                case 5:
                    ctrl5.setTokens((firstPlayer) ? 'X' : 'O');
                    break;
            }
        }

        public void playerTurn()
        {
            switch (dim)
            {
                case 3:
                    ctrl3.yourTurn();
                    break;
                case 4:
                    ctrl4.yourTurn();
                    break;
                case 5:
                    ctrl5.yourTurn();
                    break;
            }
            showMessage("Your turn");
        }

        public void endGame(string msg)
        {
            if (!ended)
            {
                ended = true;

                switch (dim)
                {
                    case 3:
                        ctrl3.stopGame();
                        ctrl3.disableBoard();
                        break;
                    case 4:
                        ctrl4.stopGame();
                        ctrl4.disableBoard();
                        break;
                    case 5:
                        ctrl5.stopGame();
                        ctrl5.disableBoard();
                        break;
                }

                showMessage(msg);
            }
        }
        
        #endregion

    }
}
