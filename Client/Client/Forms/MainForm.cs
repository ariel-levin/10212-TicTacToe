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
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{

    /* The main form of Tic Tac Toe client */
    public partial class MainForm : Form
    {
        #region Regex : constant regexes to validate form fields

        public static readonly Regex regexString    = new Regex(@"^[A-Z][a-z]+([ ][A-Z][a-z]+)*$");
        public static readonly Regex regexPhone     = new Regex(@"^[\d]+([-][\d]+)*$");

        #endregion

        #region Forms

        public RegisterForm registerForm { get; set; }
        public NewChampForm newChampForm { get; set; }
        public LoginForm loginForm { get; set; }
        public RegisterToChampForm regToChampForm { get; set; }
        public BoardForm boardForm { get; set; }
        public QueriesForm queriesForm { get; set; }
        public HistoryForm historyForm { get; set; }

        #endregion

        private TTTClient client;
        private PlayerData currentPlayer;



        public MainForm()
        {
            InitializeComponent();
            enableMenuItems(false);
            InstanceContext context = new InstanceContext(new MyCallBack(this));
            client = new TTTClient(context);

            try
            {
                client.wake();
            }
            catch (Exception)
            {
                showError("Error: Service offline");
                Dispose();
                Application.Exit();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeApp();
        }


        private void showError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void enableMenuItems(bool loggedIn)
        {
            loginMenuItem.Enabled = !loggedIn;
            registerMenuItem.Enabled = !loggedIn;
            logoutMenuItem.Enabled = loggedIn;
            championshipMenuItem.Enabled = loggedIn;
            gameMenuItem.Enabled = loggedIn;
            queriesMenuItem.Enabled = loggedIn;
        }

        private void closeApp()
        {
            try
            {
                if (currentPlayer != null)
                {
                    client.logout(false);
                    Thread.Sleep(1000);
                }
                client.Close();
            }
            catch (Exception) { }

            Application.Exit();
        }


        #region Menu Item Clicks

        private void loginMenuItem_Click(object sender, EventArgs e)
        {
            if (loginForm != null)
                showError("Login Form is already open");
            else
            {
                if (currentPlayer == null)
                {
                    loginForm = new LoginForm(this);
                    loginForm.Show();
                }
                else
                    showError("Error: Please logout first");
            }
        }

        private void registerMenuItem_Click(object sender, EventArgs e)
        {
            if (registerForm != null)
                showError("Register Form is already open");
            else
            {
                if (currentPlayer == null)
                {
                    registerForm = new RegisterForm(this);
                    registerForm.Show();
                }
                else
                    showError("Error: Please logout first");
            }
        }

        private void logoutMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPlayer != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                    "Confirmation", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {

                    logoutMenuItem.Enabled = false;
                    loginMenuItem.Enabled = false;
                    client.logout(true);
                }
            }
            else
                showError("Error: Please login first");
        }

        private void newChampMenuItem_Click(object sender, EventArgs e)
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

        private void registerChampMenuItem_Click(object sender, EventArgs e)
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

        private void online3MenuItem_Click(object sender, EventArgs e)
        {
            if (boardForm != null)
                showError("Game Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    boardForm = new BoardForm(3, this, false);
                    boardForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void online4MenuItem_Click(object sender, EventArgs e)
        {
            if (boardForm != null)
                showError("Game Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    boardForm = new BoardForm(4, this, false);
                    boardForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void online5MenuItem_Click(object sender, EventArgs e)
        {
            if (boardForm != null)
                showError("Game Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    boardForm = new BoardForm(5, this, false);
                    boardForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void offline3MenuItem_Click(object sender, EventArgs e)
        {
            if (boardForm != null)
                showError("Game Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    boardForm = new BoardForm(3, this, true);
                    boardForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void offline4MenuItem_Click(object sender, EventArgs e)
        {
            if (boardForm != null)
                showError("Game Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    boardForm = new BoardForm(4, this, true);
                    boardForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void offline5MenuItem_Click(object sender, EventArgs e)
        {
            if (boardForm != null)
                showError("Game Form is already open");
            else
            {
                if (currentPlayer != null)
                {
                    boardForm = new BoardForm(5, this, true);
                    boardForm.Show();
                }
                else
                    showError("Error: Please login first");
            }
        }

        private void historyMenuItem_Click(object sender, EventArgs e)
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

        private void queriesMenuItem_Click(object sender, EventArgs e)
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

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBox()).Show();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            closeApp();
        }

        #endregion


        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////


        #region Public Methods

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
            enableMenuItems(true);
        }

        public void playerLogout()
        {
            MessageBox.Show("Player logout successfully:\n", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            currentPlayer = null;
            lblCurrentPlayer.Text = "logged out";
            enableMenuItems(false);
        }

        public void playerLogoutError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            enableMenuItems(true);
        }

        #endregion


        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////


        #region Strings

        public string playerString(PlayerData player) {
            return player.Id + " : " + Regex.Replace(player.FirstName, @"\s+", " ");
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

        #endregion

    }


}
