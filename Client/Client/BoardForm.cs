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
        private int dim;

        private MainForm mainForm;
        private bool ended;

        private Board4Control ctrl4;


        public BoardForm(int dim, MainForm mainForm, bool singlePlayer)
        {
            InitializeComponent();

            switch (dim)
            {
                case 3:

                    break;
                case 4:
                    ctrl4 = new Board4Control(this);
                    boardElementHost.Child = ctrl4;
                    break;
                case 5:

                    break;
            }

            this.dim = dim;
            this.mainForm = mainForm;
            ended = false;
            mainForm.getClient().startGameRequest(4, singlePlayer);
        }

        private void Board4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.getClient().playerExitGame();
            mainForm.boardForm = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            mainForm.getClient().playerExitGame();
            mainForm.boardForm = null;
            Dispose();
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

                    break;
                case 4:
                    ctrl4.opponentPressed(row, col);
                    break;
                case 5:

                    break;
            }
        }

        public void startGame(bool yourTurn)
        {
            if (yourTurn)
            {
                showMessage("Game on!\nYour turn");
                playerTurn();
            }
            else
                showMessage("Game on!\nOpponent's turn");
        }

        public void showError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void showMessage(string msg)
        {
            MessageBox.Show(msg, "Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void addedSuccessfully(bool firstPlayer)
        {
            showMessage("You joined a new board successfully!");
            switch (dim)
            {
                case 3:

                    break;
                case 4:
                    ctrl4.setTokens((firstPlayer) ? 'X' : 'O');
                    break;
                case 5:

                    break;
            }
        }

        public void playerTurn()
        {
            switch (dim)
            {
                case 3:

                    break;
                case 4:
                    ctrl4.yourTurn();
                    break;
                case 5:

                    break;
            }
        }

        public void endGame(string msg)
        {
            if (!ended)
            {
                ended = true;

                switch (dim)
                {
                    case 3:

                        break;
                    case 4:
                        ctrl4.stopGame();
                        ctrl4.disableBoard();
                        break;
                    case 5:

                        break;
                }

                showMessage(msg);
            }
        }

    }
}
