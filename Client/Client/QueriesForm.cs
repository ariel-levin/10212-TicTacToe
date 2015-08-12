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
    public partial class QueriesForm : Form
    {
        private MainForm mainForm;
        private QueryControl ctrl;
        private object[] objects;

        public QueriesForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void QueriesForm_Load(object sender, EventArgs e)
        {
            initQueryCB();
            initDelTypeCB();
        }

        private void initQueryCB()
        {
            cbQuery.Items.Add("1 - Display all Players");
            cbQuery.Items.Add("2 - Display all Games");
            cbQuery.Items.Add("3 - Display all championships");
            cbQuery.Items.Add("4 - Players games");
            cbQuery.Items.Add("5 - Players championships");
            cbQuery.Items.Add("6 - Games players");
            cbQuery.Items.Add("7 - Games advisors");
            cbQuery.Items.Add("8 - Championship players");
            cbQuery.Items.Add("9 - Players number of games");
            cbQuery.Items.Add("10 - City championships");
        }

        private void initDelTypeCB()
        {
            cbDelType.Items.Add("Single row");
            cbDelType.Items.Add("Multi row");
            cbDelType.SelectedIndex = 0;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            mainForm.queriesForm = null;
            Dispose();
        }

        private void QueriesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.queriesForm = null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            switch (cbDelType.SelectedIndex)
            {
                case 0:

                    break;
                case 1:

                    break;
            }
        }

        private void cbQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbQuery.SelectedIndex)
            {
                case 0:
                    enableComponents(false, true);
                    mainForm.getClient().getAllUsers('Q');
                    break;
                case 1:
                    cbSubQuery.Enabled = false;

                    break;
                case 2:
                    cbSubQuery.Enabled = false;

                    break;
                case 3:
                    cbSubQuery.Enabled = true;
                    cbSubQuery.Items.Add("--temp--");
                    cbSubQuery.SelectedIndex = 0;
                    break;
                case 4:
                    cbSubQuery.Enabled = true;
                    cbSubQuery.Items.Add("--temp--");
                    cbSubQuery.SelectedIndex = 0;
                    break;
                case 5:
                    cbSubQuery.Enabled = true;
                    cbSubQuery.Items.Add("--temp--");
                    cbSubQuery.SelectedIndex = 0;
                    break;
                case 6:
                    cbSubQuery.Enabled = true;
                    cbSubQuery.Items.Add("--temp--");
                    cbSubQuery.SelectedIndex = 0;
                    break;
                case 7:
                    cbSubQuery.Enabled = true;
                    cbSubQuery.Items.Add("--temp--");
                    cbSubQuery.SelectedIndex = 0;
                    break;
                case 8:
                    cbSubQuery.Enabled = false;

                    break;
                case 9:
                    cbSubQuery.Enabled = false;

                    break;
            }
        }

        private void cbSubQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbQuery.SelectedIndex)
            {
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
                case 7:

                    break;
            }
        }

        private void enableComponents(bool subQuery, bool change)
        {
            cbSubQuery.Enabled = subQuery;
            cbDelType.Enabled = change;
            btnUpdate.Enabled = change;
            btnDelete.Enabled = change;
        }



        public void setAllPlayers(PlayerData[] players)
        {
            this.objects = players;
            string[] titles = { "Id", "FirstName", "LastName", "City", "Country", "Phone", "IsAdvisor" };
            string[] types = { "int", "char", "char", "char", "char", "char", "int" };
            bool[] readOnly = { true, false, false, false, false, false, false };
            bool[] allowNull = { false, false, true, true, true, true, false };
            ctrl = new QueryControl(objects, titles, types, readOnly, allowNull);
            tableElementHost.Child = ctrl;
        }

    }
}
