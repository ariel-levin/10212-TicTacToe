/*********************************  
 *  Ariel Levin
 *  ariel.lvn89@gmail.com
 *  http://about.me/ariel.levin
 *********************************/

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
using System.Windows.Interop;


namespace Client
{
    /* 3x3 board user control for WPF element host on BoardForm */
    public partial class Board3Control : UserControl
    {
        private const int DIM = 3;

        private BoardForm form;
        private Button[,] btnMatrix;
        private char playerToken, opponentToken;
        private bool wait;


        public Board3Control(BoardForm form)
        {
            InitializeComponent();
            this.form = form;
            wait = true;
            initMatrix();
        }

        private void initMatrix()
        {
            btnMatrix = new Button[DIM, DIM];
            btnMatrix[0, 0] = A1; btnMatrix[0, 1] = A2; btnMatrix[0, 2] = A3;
            btnMatrix[1, 0] = B1; btnMatrix[1, 1] = B2; btnMatrix[1, 2] = B3;
            btnMatrix[2, 0] = C1; btnMatrix[2, 1] = C2; btnMatrix[2, 2] = C3;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            showAnimation(row, col, playerToken);

            form.getClient().playerPressed(row, col);
            form.setStatus("Opponent's turn");
        }


        #region Public Methods

        public void showAnimation(int row, int col, char token)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));

            EllipseGeometry eg = new EllipseGeometry();

            Storyboard.SetTarget(da, btnMatrix[row, col]);
            Storyboard.SetTargetProperty(da, new PropertyPath(Control.OpacityProperty));
            sb.Children.Add(da);

            System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject(token.ToString().ToLower());
            ImageSource img = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            btnMatrix[row, col].Content = new Image { Source = img, VerticalAlignment = VerticalAlignment.Center };

            sb.Begin(this);
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
            showAnimation(row, col, opponentToken);
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

        #endregion

    }

}