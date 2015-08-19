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
    public partial class NewChampForm : Form
    {
        private MainForm mainForm;
        private string picPath;
        private ErrorProvider ep;
        private bool valid;


        public NewChampForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.picPath = null;
            this.ep = new ErrorProvider();
        }
        private void NewChampForm_Load(object sender, EventArgs e)
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
                ChampionshipData champ = getChampionshipFromFields();
                mainForm.getClient().registerNewChampionship(champ);
            }
            else
                MessageBox.Show("Some fields are missing or invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mainForm.newChampForm = null;
            Dispose();
        }

        private ChampionshipData getChampionshipFromFields()
        {
            ChampionshipData champ = new ChampionshipData();
            champ.City = (tbCity.Text.Length > 0) ? tbCity.Text : null;
            champ.StartDate = dtpStartDate.Value;
            if (cbHasEnded.Checked)
                champ.EndDate = dtpEndDate.Value;
            else
                champ.EndDate = null;
            if (cbPicture.Checked && picPath != null)
            {
                var uri = new System.Uri(picPath);
                champ.Picture = uri.AbsoluteUri;
            }
            else
                champ.Picture = null;
            return champ;
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog1.Title = "Please select an image file";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picPath = openFileDialog1.FileName;
                lblPicturePath.Text = picPath;
            }
        }

        private void cbHasEnded_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHasEnded.Checked)
                dtpEndDate.Enabled = true;
            else
                dtpEndDate.Enabled = false;
        }

        private void cbPicture_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPicture.Checked)
                btnPicture.Enabled = true;
            else
                btnPicture.Enabled = false;

            picPath = null;
            lblPicturePath.Text = "...";
        }

        private void NewChampForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.newChampForm = null;
        }    


        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////


        public void showNewChampSuccess()
        {
            MessageBox.Show("Championship was added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mainForm.newChampForm = null;
            Dispose();
        }

        private void tbCity_Validating(object sender, CancelEventArgs e)
        {
            bool ok = false;
            if (tbCity.Text.Length < 3)
                ep.SetError(tbCity, "Field is too short");
            else if (tbCity.Text.Length > 20)
                ep.SetError(tbCity, "Field is too long");
            else if (tbCity.Text.Any(char.IsDigit))
                ep.SetError(tbCity, "Field must not contain numbers");
            else if (!MainForm.regexString.IsMatch(tbCity.Text))
                ep.SetError(tbCity, "Must contain only characters, at least one word, and each word starts with a capital letter");
            else
                ok = true;

            if (ok)
                ep.Clear();
            else
                tbCity.Focus();

            valid &= ok;
        }

    }
}
