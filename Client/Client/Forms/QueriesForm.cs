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

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            switch (cbDelType.SelectedIndex)
            {
                case 0:     // single row

                    break;
                case 1:     // multi row

                    break;
            }
        }

        private void cbQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbQuery.SelectedIndex)
            {
                case 0:
                    enableComponents(false, true);
                    mainForm.getClient().getAllUsers("Q");
                    break;
                case 1:
                    enableComponents(false, true);
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
                        int id = players[cbSubQuery.SelectedIndex].Id;
                        mainForm.getClient().getAllGames(true, id, "Q");
                    }
                    break;
                case 4:
                    if (players != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < players.Length)
                    {
                        int id = players[cbSubQuery.SelectedIndex].Id;
                        mainForm.getClient().getAllChampionships(id, "Q");
                    }
                    break;
                case 5:
                    if (games != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < games.Length)
                    {
                        mainForm.getClient().getGamePlayers(games[cbSubQuery.SelectedIndex]);
                    }
                    break;
                case 6:
                    if (games != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < games.Length)
                    {
                        mainForm.getClient().getGameAdvisors(games[cbSubQuery.SelectedIndex]);
                    }
                    break;
                case 7:
                    if (chmps != null && cbSubQuery.SelectedIndex >= 0 && cbSubQuery.SelectedIndex < chmps.Length)
                    {
                        mainForm.getClient().getChampionshipPlayers(chmps[cbSubQuery.SelectedIndex]);
                    }
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

        public void setPlayersQuery(PlayerData[] players)
        {
            this.objects = players;
            string[] titles = { "Id", "FirstName", "LastName", "City", "Country", "Phone", "IsAdvisor" };
            string[] types = { "int", "char", "char", "char", "char", "phone", "int" };
            bool[] readOnly = { true, false, false, false, false, false, false };
            bool[] nullable = { false, false, true, true, true, true, false };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
        }

        public void setGameAdvisorsQuery(PlayerData[] advisors)
        {
            this.objects = advisors;
            string[] titles = { "Id", "FirstName", "LastName", "City", "Country", "Phone", "AdviseToName" };
            string[] types = { "int", "char", "char", "char", "char", "phone", "int" };
            bool[] readOnly = { true, false, false, false, false, false, true };
            bool[] nullable = { false, false, true, true, true, true, false };
            ctrl = new QueryControl(objects, titles, types, readOnly, nullable);
            tableElementHost.Child = ctrl;
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

    }
}
