using Client.TTTService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public partial class MainForm : Form
    {
        public TTTClient client { get; set; }
        public PlayerData currentPlayer { get; set; }
        public RegisterForm registerForm { get; set; }
        public NewChampForm newChampForm { get; set; }
        public SelectUserForm selectUserForm { get; set; }


        public MainForm()
        {
            InitializeComponent();

            MyCallBack callback = new MyCallBack(this);
            InstanceContext context = new InstanceContext(callback);
            client = new TTTClient(context);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBoard4_Click(object sender, EventArgs e)
        {
            (new BoardForm(4)).Show();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBox()).Show();
        }

        private void btnRegisterUser_Click(object sender, EventArgs e)
        {
            registerForm = new RegisterForm(this);
            registerForm.Show();
        }

        private void btnNewChamp_Click(object sender, EventArgs e)
        {
            if (currentPlayer != null)
            {
                newChampForm = new NewChampForm(this);
                newChampForm.Show();
            }
            else
                errorPlayerNotConnected();
        }

        private void btnSelectUser_Click(object sender, EventArgs e)
        {
            selectUserForm = new SelectUserForm(this);
            selectUserForm.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
        }

        private void errorPlayerNotConnected()
        {
            MessageBox.Show("Error: Please connect / register as a player first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////


        public void showException(Exception e)
        {
            MessageBox.Show(e.ToString());
        }

    }


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

        public void sendChampionships(ChampionshipData[] chmps)
        {
            throw new NotImplementedException();
        }

        public void showNewChampSuccess()
        {
            if (mainForm.newChampForm != null)
                mainForm.newChampForm.showNewChampSuccess();
        }

        public void sendAllUsers(PlayerData[] users)
        {
            if (mainForm.selectUserForm != null)
                mainForm.selectUserForm.setUsersList(users);
        }
    }

}
