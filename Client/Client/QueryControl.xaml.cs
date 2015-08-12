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
        private object[] objects;
        private string[] titles, types;
        private bool[] readOnly, allowNull;


        public QueryControl(object[] objects, string[] titles, string[] types, bool[] readOnly, bool[] allowNull)
        {
            InitializeComponent();
            initDataGrid(objects, titles, types, readOnly, allowNull);
        }

        private void initDataGrid(object[] objects, string[] titles, string[] types, bool[] readOnly, bool[] allowNull)
        {
            for (var i = 0; i < titles.Length; i++)
            {
                DataGridColumn col = null;

                switch (types[i])
                {
                    case "combobox":
                        col = createComboBoxColumn(titles[i]);
                        break;
                    case "datetime":
                        col = createDateTimeColumn(titles[i], readOnly[i]);
                        break;
                    case "image":
                        col = createImageColumn(titles[i]);
                        break;
                    default:
                        col = createDefaultColumn(titles[i], types[i], readOnly[i], allowNull[i]);
                        break;
                }
                queryDataGrid.Columns.Add(col);
            }
            queryDataGrid.ItemsSource = objects.ToList();
        }

        private DataGridColumn createDefaultColumn(string title, string type, bool readOnly, bool allowNull)
        {
            DataGridTextColumn col = new DataGridTextColumn();
            col.Header = title;
            col.IsReadOnly = readOnly;
            Binding bind = new Binding(title);
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            //switch (type)
            //{
            //    case "int":
            //        bind.ValidationRules.Add(new CharCellDataInfoValidationRule(allowNull) { ValidationStep = ValidationStep.RawProposedValue });
            //        break;
            //    case "char":

            //        break;
            //}

            col.Binding = bind;
            return col;
        }

        private DataGridColumn createComboBoxColumn(string title)
        {
            DataGridComboBoxColumn col = new DataGridComboBoxColumn();
            col.Header = title;
            Binding bind = new Binding(title);
            List<string> options = new List<string>();
            options.Add("Yes");
            options.Add("No");
            col.SelectedItemBinding = bind;
            col.ItemsSource = options;
            return col;
        }

        private DataGridColumn createDateTimeColumn(string title, bool readOnly)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();


            return col;
        }

        private DataGridColumn createImageColumn(string title)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();


            return col;
        }




        private void queryDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }



    }
}
