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
    public partial class RegisterForm : Form
    {
        private MainForm mainForm;
        private PlayerData[] players;


        public RegisterForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            mainForm.getClient().getRegisterFormAdvisorList();
        }
        

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            int[] advisors = getSelectedAdvisors();
            mainForm.getClient().registerNewPlayer(getPlayerFromFields(), advisors);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mainForm.registerForm = null;
            Dispose();
        }

        private PlayerData getPlayerFromFields()
        {
            PlayerData player = new PlayerData();
            player.FirstName = tbFirstName.Text;
            player.LastName = (tbLastName.Text.Length > 0) ? tbLastName.Text : null;
            player.City = (tbCity.Text.Length > 0) ? tbCity.Text : null;
            player.Country = (tbCountry.Text.Length > 0) ? tbCountry.Text : null;
            player.Phone = (tbPhone.Text.Length > 0) ? tbPhone.Text : null;
            player.IsAdvisor = (cbAdvisor.Checked) ? (byte)1 : (byte)0;
            return player;
        }

        private int[] getSelectedAdvisors()
        {
            if (clbAdvisors.CheckedIndices.Count < 1)
                return null;

            int[] advisors = new int[clbAdvisors.CheckedIndices.Count];
            int i = 0;
            foreach (var j in clbAdvisors.CheckedIndices.Cast<int>().ToArray())
            {
                advisors[i++] = players[j].Id;
            }
            return advisors;
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.registerForm = null;
        }



        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////


        public void setAdvisorList(PlayerData[] players)
        {
            this.players = players;
            for (var i = 0; i < players.Length; i++)
            {
                clbAdvisors.Items.Add(mainForm.playerString(players[i]));
            }
        }

        public void showPlayerRegisterSuccess()
        {
            MessageBox.Show("Player was added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mainForm.registerForm = null;
            Dispose();
        }

    }
}
