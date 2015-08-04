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
    public partial class Board4Form : Form
    {
        private MainForm mainForm;
        private Board4Control ctrl;
        private bool ended;


        public Board4Form(MainForm mainForm, bool singlePlayer)
        {
            InitializeComponent();
            ctrl = new Board4Control(this);
            boardElementHost.Child = ctrl;
            this.mainForm = mainForm;
            ended = false;
            mainForm.getClient().startGameRequest(4, singlePlayer);
        }

        private void Board4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.getClient().playerExitGame();
            mainForm.board4Form = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            mainForm.getClient().playerExitGame();
            mainForm.board4Form = null;
            Dispose();
        }


        public TTTClient getClient()
        {
            return mainForm.getClient();
        }

        public void opponentPressed(int row, int col)
        {
            ctrl.opponentPressed(row, col);
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
            if (firstPlayer)
                ctrl.setTokens('X');
            else
                ctrl.setTokens('O');
        }

        public void playerTurn()
        {
            ctrl.yourTurn();
        }

        public void endGame(string msg)
        {
            if (!ended)
            {
                ended = true;
                ctrl.stopGame();
                ctrl.disableBoard();
                showMessage(msg);
            }
        }

    }
}
