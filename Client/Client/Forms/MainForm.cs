/*********************************  
 *  Ariel Levin
 *  ariel.lvn89@gmail.com
 *  http://about.me/ariel.levin
 *********************************/

using Client.TTTService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public partial class MainForm : Form
    {
        public RegisterForm registerForm { get; set; }
        public NewChampForm newChampForm { get; set; }
        public LoginForm loginForm { get; set; }
        public RegisterToChampForm regToChampForm { get; set; }
        public BoardForm boardForm { get; set; }
        public QueriesForm queriesForm { get; set; }
        public HistoryForm historyForm { get; set; }

        private TTTClient client;
        private PlayerData currentPlayer;


        public MainForm()
        {
            InitializeComponent();
            
            InstanceContext context = new InstanceContext(new MyCallBack(this));
            client = new TTTClient(context);

            try
            {
                client.wake();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Service offline", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose();
                Application.Exit();
            }            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOnline4_Click(object sender, EventArgs e)
        {
            if (currentPlayer != null)
            {
                boardForm = new BoardForm(4, this, false);
                boardForm.Show();
            }
            else
                showError("Error: Please login first");
        }

        private void btnOffline4_Click(object sender, EventArgs e)
        {
            if (currentPlayer != null)
            {
                boardForm = new BoardForm(4, this, true);
                boardForm.Show();
            }
            else
                showError("Error: Please login first");
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBox()).Show();
        }

        private void btnRegisterUser_Click(object sender, EventArgs e)
        {
            if (registerForm != null)
                showError("Register Form is already open");
            else
            {
                registerForm = new RegisterForm(this);
                registerForm.Show();
            }
        }

        private void btnNewChamp_Click(object sender, EventArgs e)
        {
            if (newChampForm != null)
                showError("New Championship Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    newChampForm = new NewChampForm(this);
                    newChampForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (loginForm != null)
                showError("Login Form is already open");
            else
            {
                loginForm = new LoginForm(this);
                loginForm.Show();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentPlayer != null)
            {
                client.logout(false);
                Thread.Sleep(1000);
            }
            client.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                btnLogout.Enabled = false;
                btnLogin.Enabled = false;
                client.logout(true);
            }
        }

        private void btnRegToChamp_Click(object sender, EventArgs e)
        {
            if (regToChampForm != null)
                showError("Register to Championship Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    regToChampForm = new RegisterToChampForm(this);
                    regToChampForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void btnQueries_Click(object sender, EventArgs e)
        {
            if (queriesForm != null)
                showError("Queries Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    queriesForm = new QueriesForm(this);
                    queriesForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (historyForm != null)
                showError("History Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    historyForm = new HistoryForm(this);
                    historyForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void showError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////


        public TTTClient getClient()
        {
            return client;
        }

        public PlayerData getCurrentPlayer()
        {
            return currentPlayer;
        }

        public void showException(Exception e)
        {
            MessageBox.Show(e.ToString());
        }

        public void playerLogin(PlayerData player)
        {
            currentPlayer = player;
            lblCurrentPlayer.Text = playerString(player);
            btnLogin.Enabled = false;
            btnLogout.Enabled = true;
        }

        public void playerLogout()
        {
            MessageBox.Show("Player logout successfully:\n", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            currentPlayer = null;
            lblCurrentPlayer.Text = "logged out";
            btnLogout.Enabled = false;
            btnLogin.Enabled = true;
        }

        public void playerLogoutError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnLogout.Enabled = true;
            btnLogin.Enabled = false;
        }


        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////


        public string playerString(PlayerData player) {
            return player.Id + " : " + player.FirstName;
        }

        public string championshipString(ChampionshipData champ)
        {
            return champ.Id + " : " + Regex.Replace(champ.City, @"\s+", " ")
                + ": " + champ.StartDate.ToShortDateString();
        }

        public string gameString(GameData game)
        {
            return game.Id + " : Board " + game.BoardSize + " : " 
                + game.StartTime.ToShortDateString();
        }

    }


}
