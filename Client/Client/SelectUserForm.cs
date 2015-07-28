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
    public partial class SelectUserForm : Form
    {
        private MainForm mainForm;
        private PlayerData[] players;


        public SelectUserForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SelectUserForm_Load(object sender, EventArgs e)
        {
            mainForm.client.getAllUsers();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mainForm.selectUserForm = null;
            Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (listPlayers.SelectedIndices.Count < 1)
                MessageBox.Show("Error: No user is selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

            }
        }

        private void SelectUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.selectUserForm = null;
        }


        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////


        public void setUsersList(PlayerData[] players)
        {
            this.players = players;
            for (var i = 0; i < players.Length; i++)
            {
                listPlayers.Items.Add(players[i].Id + " : " + players[i].FirstName);
            }
        }

    }

}
