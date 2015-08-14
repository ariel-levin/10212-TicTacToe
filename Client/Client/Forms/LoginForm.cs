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
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public partial class LoginForm : Form
    {
        private MainForm mainForm;
        private PlayerData[] players;


        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            mainForm.getClient().getAllUsers("L");
        }

        private void SelectUserForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mainForm.loginForm = null;
            Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (listPlayers.SelectedIndices.Count < 1)
                MessageBox.Show("Error: No user is selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;
                PlayerData user = players[listPlayers.SelectedIndices.Cast<int>().First()];
                mainForm.getClient().login(user);
            }
        }

        private void SelectUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.loginForm = null;
        }


        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////


        public void setUsersList(PlayerData[] players)
        {
            this.players = players;
            for (var i = 0; i < players.Length; i++)
            {
                listPlayers.Items.Add(mainForm.playerString(players[i]));
            }
        }

        public void showPlayerLoginSuccess(PlayerData user)
        {
            MessageBox.Show("Player login successfully:\n"
                + mainForm.playerString(user), "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            mainForm.playerLogin(user);
            mainForm.loginForm = null;
            Dispose();
        }

        public void showLoginError(string error, PlayerData user)
        {
            MessageBox.Show(error + "\n" + mainForm.playerString(user), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;
        }

    }

}
