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


        public NewChampForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.picPath = null;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            ChampionshipData champ = getChampionshipFromFields();
            mainForm.client.registerNewChampionship(champ);
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
            openFileDialog1.InitialDirectory = @"C:\";
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



    }
}
