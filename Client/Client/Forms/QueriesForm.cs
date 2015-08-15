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
    public partial class QueriesForm : Form
    {
        private MainForm mainForm;
        private QueryControl ctrl;
        private object[] queryObjects, subQueryObjects;


        public QueriesForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            cbDelType.SelectedIndex = 0;
        }

        private void QueriesForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
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
            DialogResult result = MessageBox.Show("Are you sure you want to update?",
                "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                LinkedList<object> rows = ctrl.getRowsChanged<object>();

                if (rows.Any())
                {
                    if (queryObjects is PlayerData[])
                    {
                        IEnumerable<PlayerData> players = rows.Cast<PlayerData>();
                        mainForm.getClient().updatePlayers(players.ToArray());
                    }
                    else if (queryObjects is ChampionshipData[])
                    {
                        IEnumerable<ChampionshipData> chmps = rows.Cast<ChampionshipData>();
                        mainForm.getClient().updateChampionships(chmps.ToArray());
                    }
                }
                else
                    MessageBox.Show("No rows are selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete?",
                "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                switch (cbDelType.SelectedIndex)
                {
                    case 0:     // single row
                        deleteSingleRow();
                        break;
                    case 1:     // multi row
                        deleteMultiRows();
                        break;
                }
            }
        }

        private void cbQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            ctrl = null;
            tableElementHost.Child = null;

            switch (cbQuery.SelectedIndex)
            {
                case 0:
                    enableComponents(false, true);
                    mainForm.getClient().getAllUsers("Q");
                    break;
                case 1:
                    enableComponents(false, false);
                    mainForm.getClient().getAllGames(true, -1, "Q");
                    break;
                case 2:
                    enableComponents(false, true);
                    mainForm.getClient().getAllChampionships(-1, "Q");
                    break;
                case 3:
                case 4:
                    mainForm.getClient().getAllUsers("SQ");
                    break;
                case 5:
                case 6:
                    mainForm.getClient().getAllGames(false, -1, "SQ");
                    break;
                case 7:
                    mainForm.getClient().getAllChampionships(-1, "SQ");
                    break;
                case 8:
                    enableComponents(false, false);
                    mainForm.getClient().getPlayersGamesNum();
                    break;
                case 9:
                    enableComponents(false, false);
                    mainForm.getClient().getCitiesChampionshipsNum();
                    break;
            }
        }

        private void cbSubQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbQuery.SelectedIndex)
            {
                case 3:
                    if (subQueryObjects != null && subQueryObjects is PlayerData[] && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < subQueryObjects.Length)
                    {
                        enableComponents(true, false);
                        int id = ((PlayerData)subQueryObjects[cbSubQuery.SelectedIndex]).Id;
                        mainForm.getClient().getAllGames(true, id, "Q");
                    }
                    break;
                case 4:
                    if (subQueryObjects != null && subQueryObjects is PlayerData[] && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < subQueryObjects.Length)
                    {
                        enableComponents(true, true);
                        int id = ((PlayerData)subQueryObjects[cbSubQuery.SelectedIndex]).Id;
                        mainForm.getClient().getAllChampionships(id, "Q");
                    }
                    break;
                case 5:
                    if (subQueryObjects != null && subQueryObjects is GameData[] && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < subQueryObjects.Length)
                    {
                        enableComponents(true, true);
                        mainForm.getClient().getGamePlayers((GameData)subQueryObjects[cbSubQuery.SelectedIndex]);
                    }
                    break;
                case 6:
                    if (subQueryObjects != null && subQueryObjects is GameData[] && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < subQueryObjects.Length)
                    {
                        enableComponents(true, true);
                        mainForm.getClient().getGameAdvisors((GameData)subQueryObjects[cbSubQuery.SelectedIndex]);
                    }
                    break;
                case 7:
                    if (subQueryObjects != null && subQueryObjects is ChampionshipData[] && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < subQueryObjects.Length)
                    {
                        enableComponents(true, true);
                        mainForm.getClient().getChampionshipPlayers((ChampionshipData)subQueryObjects[cbSubQuery.SelectedIndex]);
                    }
                    break;
            }
        }

        private void cbDelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ctrl != null)
                ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        private void enableComponents(bool subQuery, bool change)
        {
            cbSubQuery.Enabled = subQuery;
            cbDelType.Enabled = change;
            btnUpdate.Enabled = change;
            btnDelete.Enabled = change;
        }

        private void deleteSingleRow()
        {
            int row = ctrl.getSelectedRow();
            if (row == -1)
            {
                MessageBox.Show("No row is selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (queryObjects is PlayerData[])
            {
                PlayerData player = ((PlayerData)queryObjects[row]);
                if (player.Id == 1)
                    MessageBox.Show("Deletion of Server's user is not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    mainForm.getClient().deletePlayer(player);
            }
            else if (queryObjects is ChampionshipData[])
            {
                ChampionshipData chmp = ((ChampionshipData)queryObjects[row]);
                mainForm.getClient().deleteChampionship(chmp);
            }
            
        }
       
        private void deleteMultiRows()
        {

        }

        public void refreshDataGrid()
        {
            int tmp = cbQuery.SelectedIndex;
            cbQuery.SelectedIndex = -1;
            cbQuery.SelectedIndex = tmp;
        }

        public void setPlayersQuery(PlayerData[] players)
        {
            this.queryObjects = players;
            string[] titles = { "Id", "FirstName", "LastName", "City", "Country", "Phone", "Is_Advisor" };
            string[] types = { "int", "char", "char", "char", "char", "phone", "combobox" };
            bool[] readOnly = { true, false, false, false, false, false, false };
            bool[] nullable = { false, false, true, true, true, true, false };
            ctrl = new QueryControl(queryObjects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setGameAdvisorsQuery(PlayerData[] advisors)
        {
            this.queryObjects = advisors;
            string[] titles = { "Id", "FirstName", "LastName", "City", "Country", "Phone", "AdviseTo_Name" };
            string[] types = { "int", "char", "char", "char", "char", "phone", "int" };
            bool[] readOnly = { true, false, false, false, false, false, false };
            bool[] nullable = { false, false, true, true, true, true, true };
            ctrl = new QueryControl(queryObjects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setGamesQuery(GameData[] games)
        {
            this.queryObjects = games;
            string[] titles = { "Id", "Player1_Name", "Player2_Name", "Winner_Name", "BoardSize", "StartTime", "EndTime" };
            string[] types = { "int", "char", "char", "char", "int", "datetime", "datetime" };
            bool[] readOnly = { true, false, false, false, true, false, false };
            bool[] nullable = { false, false, false, true, false, false, false };
            ctrl = new QueryControl(queryObjects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setChampionshipsQuery(ChampionshipData[] chmps)
        {
            this.queryObjects = chmps;
            string[] titles = { "Id", "City", "StartDate", "EndDate", "Picture" };
            string[] types = { "int", "char", "datetime", "datetime", "image" };
            bool[] readOnly = { true, false, false, false, false };
            bool[] nullable = { false, false, false, true, true };
            ctrl = new QueryControl(queryObjects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setPlayersGamesNum(PlayerGames[] playersGames)
        {
            this.queryObjects = playersGames;
            string[] titles = { "Name", "NumberOfGames" };
            string[] types = { "char", "int" };
            bool[] readOnly = { true, true };
            bool[] nullable = { false, false };
            ctrl = new QueryControl(queryObjects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setCitiesChampionshipsNum(CityChampionships[] citiesChmps)
        {
            this.queryObjects = citiesChmps;
            string[] titles = { "City", "NumberOfChampionships" };
            string[] types = { "char", "int" };
            bool[] readOnly = { true, true };
            bool[] nullable = { false, false };
            ctrl = new QueryControl(queryObjects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setSubQueryObjects(object[] objects)
        {
            this.subQueryObjects = objects;
            cbSubQuery.Items.Clear();
            for (var i = 0; i < objects.Length; i++)
            {
                if (objects is PlayerData[])
                    cbSubQuery.Items.Add(mainForm.playerString((PlayerData)objects[i]));
                else if (objects is GameData[])
                    cbSubQuery.Items.Add(mainForm.gameString((GameData)objects[i]));
                else if (objects is ChampionshipData[])
                    cbSubQuery.Items.Add(mainForm.championshipString((ChampionshipData)objects[i]));
            }
            enableComponents(true, false);
        }

        public void updateSuccess()
        {
            MessageBox.Show("Database updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshDataGrid();
        }

        public void updateError(string err)
        {
            MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            refreshDataGrid();
        }

    }
}
