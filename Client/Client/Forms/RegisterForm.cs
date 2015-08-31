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
    public partial class RegisterForm : Form
    {
        private MainForm mainForm;
        private PlayerData[] players;
        private ErrorProvider ep;
        private bool valid;


        public RegisterForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            mainForm.getClient().getAdvisorList();
            ep = new ErrorProvider();
        }


        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
        }
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            valid = true;
            this.ValidateChildren();
            if (valid)
            {
                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;
                int[] advisors = getSelectedAdvisors();
                mainForm.getClient().registerNewPlayer(getPlayerFromFields(), advisors);
            }
            else
                MessageBox.Show("Some fields are missing or invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        #region Public Methods

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

        private void textMust_Validating(object sender, CancelEventArgs e)
        {
            bool ok = false;
            if (((TextBox)sender).Text.Length < 3)
                ep.SetError((TextBox)sender, "Field is too short");
            else if (((TextBox)sender).Text.Length > 20)
                ep.SetError((TextBox)sender, "Field is too long");
            else if (((TextBox)sender).Text.Any(char.IsDigit))
                ep.SetError((TextBox)sender, "Field must not contain numbers");
            else if (!MainForm.regexString.IsMatch(((TextBox)sender).Text))
                ep.SetError((TextBox)sender, "Must contain only characters, at least one word, and each word starts with a capital letter");
            else
                ok = true;

            if (ok)
                ep.Clear();
            else
                ((TextBox)sender).Focus();

            valid &= ok;
        }

        private void text_Validating(object sender, CancelEventArgs e)
        {
            bool ok = false;
            if (((TextBox)sender).Text.Length == 0)
                ok = true;
            else if (((TextBox)sender).Text.Length < 3)
                ep.SetError((TextBox)sender, "Field is too short");
            else if (((TextBox)sender).Text.Length > 20)
                ep.SetError((TextBox)sender, "Field is too long");
            else if (((TextBox)sender).Text.Any(char.IsDigit))
                ep.SetError((TextBox)sender, "Field must not contain numbers");
            else if (!MainForm.regexString.IsMatch(((TextBox)sender).Text))
                ep.SetError((TextBox)sender, "Must contain only characters, at least one word, and each word starts with a capital letter");
            else
                ok = true;

            if (ok)
                ep.Clear();
            else
                ((TextBox)sender).Focus();

            valid &= ok;
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            bool ok = false;
            if (((TextBox)sender).Text.Length == 0)
                ok = true;
            else if (!MainForm.regexPhone.IsMatch(tbPhone.Text))
                ep.SetError(tbPhone, "Phone number can only contain numbers and '-'");
            else if (tbPhone.Text.Length < 9)
                ep.SetError(tbPhone, "Phone is too short");
            else if (tbPhone.Text.Length > 20)
                ep.SetError(tbPhone, "Phone is too long");
            else
                ok = true;

            if (ok)
                ep.Clear();
            else
                tbPhone.Focus();

            valid &= ok;
        }

        #endregion

    }

}
