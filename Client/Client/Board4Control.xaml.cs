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
using System.Windows.Media.Animation;


namespace Client
{

    public partial class Board4Control : UserControl
    {
        private const int DIM = 4;

        private BoardForm form;
        private StackPanel[,] pnlMatrix;
        private char playerToken, opponentToken;
        private bool wait;


        public Board4Control(BoardForm form)
        {
            InitializeComponent();
            this.form = form;
            wait = true;
            initMatrix();
        }

        private void initMatrix()
        {
            pnlMatrix = new StackPanel[DIM, DIM];
            pnlMatrix[0, 0] = A1; pnlMatrix[0, 1] = A2; pnlMatrix[0, 2] = A3; pnlMatrix[0, 3] = A4;
            pnlMatrix[1, 0] = B1; pnlMatrix[1, 1] = B2; pnlMatrix[1, 2] = B3; pnlMatrix[1, 3] = B4;
            pnlMatrix[2, 0] = C1; pnlMatrix[2, 1] = C2; pnlMatrix[2, 2] = C3; pnlMatrix[2, 3] = C4;
            pnlMatrix[3, 0] = D1; pnlMatrix[3, 1] = D2; pnlMatrix[3, 2] = D3; pnlMatrix[3, 3] = D4;
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

        private void mouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (wait)
            {
                MessageBox.Show("Please wait..");
                return;
            }

            wait = true;

            int row = Grid.GetRow((StackPanel)sender);
            int col = Grid.GetColumn((StackPanel)sender);

            pnlMatrix[row, col].IsEnabled = false;
            showAnimation(pnlMatrix[row, col], false);

            form.getClient().playerPressed(row, col);
        }

        private void showAnimation(StackPanel pnl, bool isOpponent)
        {
            // implement animation

            //PathGeometry pg = new PathGeometry();
            
            if (isOpponent)
            {

            }
            else
            {

            }
        }
        
        public void disableBoard()
        {
            for (var i = 0; i < pnlMatrix.GetLength(0); i++)
                for (var j = 0; j < pnlMatrix.GetLength(1); j++)
                    pnlMatrix[i, j].IsEnabled = false;
        }

        public void opponentPressed(int row, int col)
        {
            pnlMatrix[row, col].IsEnabled = false;
            showAnimation(pnlMatrix[row, col], true);
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

