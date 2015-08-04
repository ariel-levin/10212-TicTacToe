using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.TTTService;


namespace Client
{

    public partial class Board4Control : UserControl
    {
        private Board4Form form;
        private Button[,] btnMatrix;
        private char playerToken, opponentToken;
        private bool wait;


        public Board4Control(Board4Form form)
        {
            InitializeComponent();
            this.form = form;
            wait = true;
            initMatrices();
        }

        private void initMatrices()
        {
            btnMatrix = new Button[4, 4];
            btnMatrix[0, 0] = A1; btnMatrix[0, 1] = A2; btnMatrix[0, 2] = A3; btnMatrix[0, 3] = A4;
            btnMatrix[1, 0] = B1; btnMatrix[1, 1] = B2; btnMatrix[1, 2] = B3; btnMatrix[1, 3] = B4;
            btnMatrix[2, 0] = C1; btnMatrix[2, 1] = C2; btnMatrix[2, 2] = C3; btnMatrix[2, 3] = C4;
            btnMatrix[3, 0] = D1; btnMatrix[3, 1] = D2; btnMatrix[3, 2] = D3; btnMatrix[3, 3] = D4;
        }

        //private void cnvs_Click(object sender, RoutedEventArgs e)
        //{
        //    var myanim = new DoubleAnimation(0, 250, new Duration(TimeSpan.FromSeconds(5)));
        //    myanim.Completed += myanim_Completed;
        //    rect1.BeginAnimation(Rectangle.WidthProperty, myanim);

        //    var myanim2 = new ColorAnimation(Colors.Yellow, Colors.Red, new Duration(TimeSpan.FromSeconds(5)));
        //    SolidColorBrush b = new SolidColorBrush();
        //    rect1.Fill = b;
        //    b.BeginAnimation(SolidColorBrush.ColorProperty, myanim2);
        //}



        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (wait) 
            {
                MessageBox.Show("Please wait..");
                return;
            }

            wait = true;
                
            int row = Grid.GetRow((Button)sender);
            int col = Grid.GetColumn((Button)sender);

            btnMatrix[row, col].IsEnabled = false;
            showAnimation(btnMatrix[row, col], false);

            form.getClient().playerPressed(row, col, 4);
        }

        private void showAnimation(Button btn, bool isOpponent)
        {
            btn.Content = (isOpponent) ? opponentToken : playerToken;
        }
        
        public void disableBoard()
        {
            for (var i = 0; i < btnMatrix.GetLength(0); i++)
                for (var j = 0; j < btnMatrix.GetLength(1); j++)
                    btnMatrix[i, j].IsEnabled = false;
        }

        public void opponentPressed(int row, int col)
        {
            btnMatrix[row, col].IsEnabled = false;
            showAnimation(btnMatrix[row, col], true);
        }

        public void yourTurn()
        {
            wait = false;
        }

        public void stopGame()
        {
            wait = true;
        }

        public void setTokens(char playerToken)
        {
            this.playerToken = playerToken;
            this.opponentToken = (playerToken == 'X') ? 'O' : 'X';
        }

    }
}
