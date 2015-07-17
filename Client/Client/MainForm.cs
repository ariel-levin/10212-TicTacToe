using Client.TTTService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public partial class MainForm : Form
    {
        private TTTClient client;

        public MainForm()
        {
            InitializeComponent();

            MyCallBack callback = new MyCallBack(this);
            InstanceContext context = new InstanceContext(callback);
            client = new TTTClient(context);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //client.Add(textBox1.Text);
            (new BoardForm(4)).Show();
        }

        public void setLabelText(string str)
        {
            label1.Text = str;
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBox()).Show();
        }

    }


    public class MyCallBack : ITTTCallback
    {
        private MainForm mainForm;

        public MyCallBack(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void Result(string res)
        {
            mainForm.setLabelText(res);
            //MessageBox.Show("test");
        }
    }

}
