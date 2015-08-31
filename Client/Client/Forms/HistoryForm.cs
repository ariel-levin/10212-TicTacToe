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
    public partial class HistoryForm : Form
    {
        private MainForm mainForm;
        private GameData[] games;


        public HistoryForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            mainForm.getClient().getAllGames(true, -1, "H", false);
        }


        private void HistoryForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
        }

        private void HistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.historyForm = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            mainForm.historyForm = null;
            Dispose();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (cbGames.SelectedIndex == -1)
                showError("Please select a game first");
            else if (games == null)
                showError("Something went wrong and the games are not loaded.\nPlease exit this form and try again");
            else
            {
                (new BoardForm(games[cbGames.SelectedIndex], mainForm)).Show();
            }
        }

        public void showError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void showMessage(string msg)
        {
            MessageBox.Show(msg, "Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void setGames(GameData[] games)
        {
            this.games = games;
            if (games != null && games.Length > 0)
            {
                for (var i = 0; i < games.Length; i++)
                {
                    string str = string.Format("{0}  |  Board {1}x{1}  |  {2}  vs.  {3}  |  {4}", 
                        games[i].Id, games[i].BoardSize,
                        "[" + games[i].Player1_Name.Replace(" ", string.Empty) + "]",
                        "[" + games[i].Player2_Name.Replace(" ", string.Empty) + "]",
                        games[i].StartTime.ToShortDateString());

                    cbGames.Items.Add(str);
                }
            }
        }

    }
}
