﻿/*********************************  
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
    public partial class RegisterToChampForm : Form
    {
        private MainForm mainForm;
        private ChampionshipData[] championships;


        public RegisterToChampForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            mainForm.getClient().getAllChampionships(-1, "R", false);
        }


        private void RegisterToChampForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ChampionshipData[] chmps = getSelectedChamps();
            if (chmps == null)
                MessageBox.Show("Error: No championships are selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;
                mainForm.getClient().registerPlayerToChamp(mainForm.getCurrentPlayer(), chmps);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mainForm.regToChampForm = null;
            Dispose();
        }

        private ChampionshipData[] getSelectedChamps()
        {
            if (clbChampionships.CheckedIndices.Count < 1)
                return null;

            ChampionshipData[] chmps = new ChampionshipData[clbChampionships.CheckedIndices.Count];
            int i = 0;
            foreach (var j in clbChampionships.CheckedIndices.Cast<int>().ToArray())
            {
                chmps[i++] = championships[j];
            }
            return chmps;
        }

        private void RegisterToChampForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.regToChampForm = null;
        }


        /////////////////////////////////////////////////////
        /////////////////////////////////////////////////////


        #region Public Methods

        public void setChampionshipsList(ChampionshipData[] chmps)
        {
            this.championships = chmps;
            for (var i = 0; i < chmps.Length; i++)
            {
                clbChampionships.Items.Add(mainForm.championshipString(chmps[i]));
            }
        }

        public void showRegToChampSuccess()
        {
            MessageBox.Show("Player was registered to championships successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mainForm.regToChampForm = null;
            Dispose();
        }

        public void showRegToChampError(string error)
        {
            MessageBox.Show(error, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;
        }

        #endregion

    }

}
