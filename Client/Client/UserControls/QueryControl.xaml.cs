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
        private string cellText, colTitle, selectedValue;
        private int rowIndex;


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
                    bind.ValidationRules.Add(new IntValidationRule(false, nullable) { ValidationStep = ValidationStep.RawProposedValue });
                    break;
                case "bit":
                    bind.ValidationRules.Add(new IntValidationRule(true, nullable) { ValidationStep = ValidationStep.RawProposedValue });
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

        private DataGridComboBoxColumn createComboBoxColumn(string title)
        {
            DataGridComboBoxColumn col = new DataGridComboBoxColumn();
            col.Header = title;
            Binding bind = new Binding(title);
            List<string> items = new List<string>();
            items.Add("Yes");
            items.Add("No");
            col.SelectedItemBinding = bind;
            col.ItemsSource = items;
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
                        this.cellText = uri.AbsoluteUri;
                        ((ChampionshipData)((ContentPresenter)cell.Content).Content).Picture = uri.AbsoluteUri;
                    }
                }
            }
        }

        // Method save the old value of the cell
        private void queryDataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            if (e.EditingElement is ContentPresenter)
            {
                var cp = e.EditingElement as ContentPresenter;
                DataTemplate temp = cp.ContentTemplate;
                Image img = temp.FindName("image", cp) as Image;
                if (img != null)
                    this.cellText = (img.Source == null) ? "" : img.Source.ToString();
                else
                {
                    DatePicker dp = temp.FindName("datepicker", cp) as DatePicker;
                    if (dp != null)
                        this.cellText = dp.Text;
                }
            }
            else if (e.EditingElement is TextBox)
            {
                //var tb = e.EditingElement as TextBox;
                //this.cellText = tb.Text;
                this.cellText = ((TextBox)e.EditingElement).Text;
            }
            else if (e.EditingElement is ComboBox)
            {
                //var box = e.EditingElement as ComboBox;
                //this.cellText = box.Text;
                this.cellText = ((ComboBox)e.EditingElement).Text;
            }
            else if (e.EditingElement is DatePicker)
            {
                //var dp = e.EditingElement as DatePicker;
                //this.cellText = dp.Text;
                this.cellText = ((DatePicker)e.EditingElement).Text;
            }
        }

        private void queryDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (cellText == null)
                return;

            int row = e.Row.GetIndex();

            if (e.EditingElement is ContentPresenter)
            {
                string newValue = "";
                //var cp = e.EditingElement as ContentPresenter;
                ContentPresenter cp = (ContentPresenter)e.EditingElement;
                DataTemplate temp = cp.ContentTemplate;
                Image img = temp.FindName("image", cp) as Image;
                if (img != null)
                    newValue = img.Source == null ? "" : img.Source.ToString();
                else
                {
                    DatePicker dp = temp.FindName("datepicker", cp) as DatePicker;
                    if (dp != null)
                        newValue = dp.Text;
                }

                if (!cellText.Equals(newValue) && !rowsChanged.Contains(row))
                    rowsChanged.AddLast(row);
            }
            else if (e.EditingElement is TextBox)
            {
                var t = e.EditingElement as TextBox;
                if (!t.Text.Equals(cellText) && !rowsChanged.Contains(row))
                    rowsChanged.AddLast(row);
            }
            else if (e.EditingElement is ComboBox)
            {
                var box = e.EditingElement as ComboBox;
                if (!cellText.Equals(box.Text) && !rowsChanged.Contains(row))
                    rowsChanged.AddLast(row);
            }
        }

        private void queryDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (queryDataGrid.SelectionUnit == DataGridSelectionUnit.Cell)
            {
                foreach (var item in e.AddedCells)
                {
                    //var col = item.Column as DataGridColumn;
                    DataGridColumn col = item.Column;
                    colTitle = col.Header.ToString();
                    rowIndex = queryDataGrid.Items.IndexOf(queryDataGrid.SelectedCells[0].Item);

                    var cc = col.GetCellContent(item.Item);

                    if (cc is TextBlock)
                    {
                        //selectedValue = (cc as TextBlock).Text;
                        selectedValue = ((TextBlock)cc).Text;
                    }
                    else if (cc is ComboBox)
                    {
                        //selectedValue = (cc as ComboBox).Text;
                        selectedValue = ((ComboBox)cc).Text;
                    }
                    else if (cc is DatePicker)
                    {
                        //selectedValue = (cc as DatePicker).Text;
                        selectedValue = ((DatePicker)cc).Text;
                    }
                }
            }
        }


        public LinkedList<object> getRowsChanged<T>()
        {
            LinkedList<object> objectsRows = new LinkedList<object>();

            foreach (var r in rowsChanged)
            {
                objectsRows.AddLast((object)queryDataGrid.Items.GetItemAt(r));
            }

            return objectsRows;
        }


        public void setSelectionType(bool multi)
        {
            queryDataGrid.SelectionUnit = (multi) ? DataGridSelectionUnit.Cell : DataGridSelectionUnit.FullRow;
        }

        public int getSelectedRow()
        {
            return queryDataGrid.SelectedIndex;
        }

    }
}
