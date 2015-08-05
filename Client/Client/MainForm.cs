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
    public partial class MainForm : Form
    {
        public RegisterForm registerForm { get; set; }
        public NewChampForm newChampForm { get; set; }
        public LoginForm loginForm { get; set; }
        public RegisterToChampForm regToChampForm { get; set; }
        public BoardForm boardForm { get; set; }

        private TTTClient client;
        private PlayerData currentPlayer;


        public MainForm()
        {
            InitializeComponent();
            
            MyCallBack callback = new MyCallBack(this);
            InstanceContext context = new InstanceContext(callback);
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



    }


    /////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////


    public class MyCallBack : ITTTCallback
    {
        private MainForm mainForm;


        public MyCallBack(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void showException(Exception e)
        {
            mainForm.showException(e);
        }

        public void sendRegisterFormAdvisorList(PlayerData[] players)
        {
            if (mainForm.registerForm != null)
                mainForm.registerForm.setAdvisorList(players);
        }

        public void showPlayerRegisterSuccess()
        {
            if (mainForm.registerForm != null)
                mainForm.registerForm.showPlayerRegisterSuccess();
        }

        public void showNewChampSuccess()
        {
            if (mainForm.newChampForm != null)
                mainForm.newChampForm.showNewChampSuccess();
        }

        public void sendAllUsers(PlayerData[] users)
        {
            if (mainForm.loginForm != null)
                mainForm.loginForm.setUsersList(users);
        }

        public void loginSuccess(PlayerData user)
        {
            if (mainForm.loginForm != null)
                mainForm.loginForm.showPlayerLoginSuccess(user);
        }

        public void loginError(string error, PlayerData user)
        {
            if (mainForm.loginForm != null)
                mainForm.loginForm.showLoginError(error, user);
        }

        public void logoutSuccess()
        {
            mainForm.playerLogout();
        }

        public void logoutError(string error)
        {
            mainForm.playerLogoutError(error);
        }

        public void response()
        {
            
        }

        public void sendRegToChampList(ChampionshipData[] chmps)
        {
            if (mainForm.regToChampForm != null)
                mainForm.regToChampForm.setChampionshipsList(chmps);
        }

        public void registerPlayerToChampSuccess()
        {
            if (mainForm.regToChampForm != null)
                mainForm.regToChampForm.showRegToChampSuccess();
        }

        public void registerPlayerToChampError(string error)
        {
            if (mainForm.regToChampForm != null)
                mainForm.regToChampForm.showRegToChampError(error);
        }

        public void startGame(bool yourTurn)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.startGame(yourTurn);
        }

        public void gameError(string error)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.showError(error);
        }

        public void gameMessage(string msg)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.showMessage(msg);
        }

 
        public void opponentPressed(int row, int col)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.opponentPressed(row, col);
        }

        public void addedSuccessfully(bool firstPlayer)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.addedSuccessfully(firstPlayer);
        }

        public void gameEnded(string msg)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.endGame(msg);
        }

        public void playerTurn()
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.playerTurn();
        }
    }

}
