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
        private object[] objects;
        private PlayerData[] players;
        private GameData[] games;
        private ChampionshipData[] chmps;


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
            LinkedList<object> list = ctrl.getRowsChanged<object>();

            if (list.Any()) // check if any row selected
            {
                if (objects is PlayerData[])
                {
                    IEnumerable<PlayerData> plst = list.Cast<PlayerData>();
                    mainForm.getClient().updatePlayers(plst.ToArray());
                }
                //else if (sender is AdviserObject[])
                //{
                //    IEnumerable<AdviserObject> adlst = list.Cast<AdviserObject>();
                //    clientService.updateAdviserDB(adlst.ToArray(), mainForm.UserName);
                //}
                //else if (sender is ChampsObject[])
                //{
                //    IEnumerable<ChampsObject> clst = list.Cast<ChampsObject>();
                //    clientService.updateChampsDB(clst.ToArray(), mainForm.UserName);
                //}
            }
            else
                MessageBox.Show("No row selected!", "Abort Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    if (players != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < players.Length)
                    {
                        enableComponents(true, false);
                        int id = players[cbSubQuery.SelectedIndex].Id;
                        mainForm.getClient().getAllGames(true, id, "Q");
                    }
                    break;
                case 4:
                    if (players != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < players.Length)
                    {
                        enableComponents(true, true);
                        int id = players[cbSubQuery.SelectedIndex].Id;
                        mainForm.getClient().getAllChampionships(id, "Q");
                    }
                    break;
                case 5:
                    if (games != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < games.Length)
                    {
                        enableComponents(true, true);
                        mainForm.getClient().getGamePlayers(games[cbSubQuery.SelectedIndex]);
                    }
                    break;
                case 6:
                    if (games != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < games.Length)
                    {
                        enableComponents(true, true);
                        mainForm.getClient().getGameAdvisors(games[cbSubQuery.SelectedIndex]);
                    }
                    break;
                case 7:
                    if (chmps != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < chmps.Length)
                    {
                        enableComponents(true, true);
                        mainForm.getClient().getChampionshipPlayers(chmps[cbSubQuery.SelectedIndex]);
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
            //try
            //{
            //    if (objects is AdviserObject[])
            //    {
            //        AdviserObject adviser = ((AdviserObject)obj[queryDataGrid.getSelectedRow()]);
            //        clientService.deleteOneRowFromAdvisers(adviser, mainForm.UserName);
            //    }
            //    else if (obj is PlayerObject[])
            //    {
            //        PlayerObject player = ((PlayerObject)obj[queryDataGrid.getSelectedRow()]);
            //        if (player.UserName.Equals("Computer"))
            //            MessageBox.Show("You can't delete computer!", "Abort Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        else
            //            clientService.deleteOneRowFromPlayers(player, mainForm.UserName);
            //    }
            //    else if (obj is ChampsObject[])
            //    {
            //        ChampsObject champ = ((ChampsObject)obj[queryDataGrid.getSelectedRow()]);
            //        clientService.deleteOneRowFromChamps(champ, mainForm.UserName);
            //    }
            //}
            //catch (IndexOutOfRangeException ex)
            //{
            //    MessageBox.Show("No row selected!", "Abort Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
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
            this.objects = players;
            string[] titles = { "Id", "FirstName", "LastName", "City", "Country", "Phone", "Is_Advisor" };
            string[] types = { "int", "char", "char", "char", "char", "phone", "combobox" };
            bool[] readOnly = { true, false, false, false, false, false, false };
            bool[] nullable = { false, false, true, true, true, true, false };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setGameAdvisorsQuery(PlayerData[] advisors)
        {
            this.objects = advisors;
            string[] titles = { "Id", "FirstName", "LastName", "City", "Country", "Phone", "AdviseTo_Name" };
            string[] types = { "int", "char", "char", "char", "char", "phone", "int" };
            bool[] readOnly = { true, false, false, false, false, false, false };
            bool[] nullable = { false, false, true, true, true, true, true };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setGamesQuery(GameData[] games)
        {
            this.objects = games;
            string[] titles = { "Id", "Player1_Name", "Player2_Name", "Winner_Name", "BoardSize", "StartTime", "EndTime" };
            string[] types = { "int", "char", "char", "char", "int", "datetime", "datetime" };
            bool[] readOnly = { true, false, false, false, true, false, false };
            bool[] nullable = { false, false, false, true, false, false, false };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setChampionshipsQuery(ChampionshipData[] chmps)
        {
            this.objects = chmps;
            string[] titles = { "Id", "City", "StartDate", "EndDate", "Picture" };
            string[] types = { "int", "char", "datetime", "datetime", "image" };
            bool[] readOnly = { true, false, false, false, false };
            bool[] nullable = { false, false, false, true, true };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setPlayersGamesNum(PlayerGames[] playersGames)
        {
            this.objects = playersGames;
            string[] titles = { "Name", "NumberOfGames" };
            string[] types = { "char", "int" };
            bool[] readOnly = { true, true };
            bool[] nullable = { false, false };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setCitiesChampionshipsNum(CityChampionships[] citiesChmps)
        {
            this.objects = citiesChmps;
            string[] titles = { "City", "NumberOfChampionships" };
            string[] types = { "char", "int" };
            bool[] readOnly = { true, true };
            bool[] nullable = { false, false };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
            ctrl.setSelectionType(cbDelType.SelectedIndex == 1);
        }

        public void setPlayersSubQuery(PlayerData[] players)
        {
            this.players = players;
            cbSubQuery.Items.Clear();
            for (var i = 0; i < players.Length; i++)
            {
                cbSubQuery.Items.Add(mainForm.playerString(players[i]));
            }
            enableComponents(true, true);
        }

        public void setGamesSubQuery(GameData[] games)
        {
            this.games = games;
            cbSubQuery.Items.Clear();
            for (var i = 0; i < games.Length; i++)
            {
                cbSubQuery.Items.Add(mainForm.gameString(games[i]));
            }
            enableComponents(true, true);
        }

        public void setChampionshipsSubQuery(ChampionshipData[] chmps)
        {
            this.chmps = chmps;
            cbSubQuery.Items.Clear();
            for (var i = 0; i < chmps.Length; i++)
            {
                cbSubQuery.Items.Add(mainForm.championshipString(chmps[i]));
            }
            enableComponents(true, true);
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
