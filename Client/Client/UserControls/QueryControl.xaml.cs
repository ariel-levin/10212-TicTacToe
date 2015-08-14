using Client.TTTService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


namespace Client
{
    
    public partial class QueryControl : UserControl
    {
        private LinkedList<int> rowsChanged = new LinkedList<int>();


        public QueryControl(object[] objects, string[] titles, string[] types, bool[] readOnly, bool[] nullable)
        {
            InitializeComponent();
            initDataGrid(objects, titles, types, readOnly, nullable);
        }

        private void initDataGrid(object[] objects, string[] titles, string[] types, bool[] readOnly, bool[] nullable)
        {
            for (var i = 0; i < titles.Length; i++)
            {
                DataGridColumn col = null;

                switch (types[i])
                {
                    case "datetime":
                        col = createDateTimeColumn(titles[i], readOnly[i]);
                        break;
                    case "image":
                        col = createImageColumn(titles[i]);
                        break;
                    default:
                        col = createDefaultColumn(titles[i], types[i], readOnly[i], nullable[i]);
                        break;
                }
                queryDataGrid.Columns.Add(col);
            }
            queryDataGrid.ItemsSource = objects.ToList();
        }

        private DataGridColumn createDefaultColumn(string title, string type, bool readOnly, bool nullable)
        {
            DataGridTextColumn col = new DataGridTextColumn();
            col.Header = title;
            col.IsReadOnly = readOnly;
            Binding bind = new Binding(title);
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            switch (type)
            {
                case "int":
                    bind.ValidationRules.Add(new IntValidationRule(nullable) { ValidationStep = ValidationStep.RawProposedValue });
                    break;
                case "char":
                    bind.ValidationRules.Add(new CharValidationRule(nullable) { ValidationStep = ValidationStep.RawProposedValue });
                    break;
                case "phone":
                    bind.ValidationRules.Add(new PhoneValidationRule(nullable) { ValidationStep = ValidationStep.RawProposedValue });
                    break;
            }

            col.Binding = bind;
            return col;
        }

        private DataGridColumn createDateTimeColumn(string title, bool readOnly)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();
            col.Header = title;
            col.IsReadOnly = readOnly;
            Binding bind = new Binding(title);
            bind.Mode = BindingMode.TwoWay;
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            FrameworkElementFactory ef = new FrameworkElementFactory(typeof(DatePicker));
            ef.Name = "datepicker";
            ef.SetBinding(DatePicker.SelectedDateProperty, bind);
            DataTemplate cellEditingTemplate = new DataTemplate();
            cellEditingTemplate.VisualTree = ef;
            col.CellEditingTemplate = cellEditingTemplate;

            FrameworkElementFactory fv = new FrameworkElementFactory(typeof(TextBlock));
            fv.SetValue(TextBlock.TextProperty, bind);
            DataTemplate temp = new DataTemplate();
            temp.VisualTree = fv;
            col.CellTemplate = temp;

            return col;
        }

        private DataGridColumn createImageColumn(string title)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();
            col.Header = title;
            col.Width = 200;

            FrameworkElementFactory ef = new FrameworkElementFactory(typeof(Image));
            ef.Name = "image";
            ef.SetBinding(Image.SourceProperty, new Binding(title));

            DataTemplate temp = new DataTemplate();
            temp.VisualTree = ef;
            col.CellTemplate = temp;

            Style style = new Style(typeof(DataGridCell));
            style.Setters.Add(new EventSetter(DataGridCell.MouseDoubleClickEvent, new MouseButtonEventHandler(imgSelect_DoubleClickEvent)));
            col.CellStyle = style;

            return col;
        }

        private void imgSelect_DoubleClickEvent(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            if (cell != null)
            {
                string[] fileType = { ".jpeg", ".png", ".jpg", ".gif" };
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                fileDialog.InitialDirectory = @"C:\";
                fileDialog.Title = "Please select an image file";

                System.Windows.Forms.DialogResult result = fileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
                {
                    string picPath = fileDialog.FileName;
                    string type = System.IO.Path.GetExtension(picPath);
                    if (fileType.Contains(type))
                    {
                        var uri = new System.Uri(picPath);
                        //var converted = uri.AbsoluteUri;
                        //this.cellText = converted;
                        ((ChampionshipData)((ContentPresenter)cell.Content).Content).Picture = uri.AbsoluteUri;
                    }
                }
            }
        }




        private void queryDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }



    }
}
