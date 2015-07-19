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

        public void setAdvisorList(PlayerData[] players)
        {
            foreach (var p in players)
            {
                clbAdvisors.Items.Add(p.Id + " : " + p.FirstName);
                //MessageBox.Show(p.Id + " : " + p.FirstName);
            }
        }

    }
}
