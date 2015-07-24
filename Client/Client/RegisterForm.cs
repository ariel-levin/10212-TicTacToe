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


        public RegisterForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            mainForm.client.getRegisterFormAdvisorList();
        }
        

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            PlayerData player = getPlayerFromFields();
            mainForm.client.registerNewPlayer(player);
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
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
            if (tbPhone.Text.Length > 0)
                player.Phone = int.Parse(tbPhone.Text);
            player.IsAdvisor = (cbAdvisor.Checked) ? (byte)1 : (byte)0;

            return player;
        }


        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////


        public void setAdvisorList(PlayerData[] players)
        {
            foreach (var p in players)
            {
                clbAdvisors.Items.Add(p.Id + " : " + p.FirstName);
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
