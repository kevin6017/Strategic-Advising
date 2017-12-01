using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Strategic_Advising
{
    /// <summary>
    /// Interaction logic for EditSelector.xaml
    /// </summary>
    public partial class EditSelector : Page
    {
        public EditSelector(string passedFilePath)
        {
            InitializeComponent();
            filePath = passedFilePath;
        }

        DataGridView dgv;
        DataTable dataTable;
        List<Course> JSONclasses;
        string filePath;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable = new DataTable("editSelectorTable");
            DataColumn dc1 = new DataColumn("Course Number", typeof(string));
            DataColumn dc2 = new DataColumn("Course Title", typeof(string));
            dataTable.Columns.Add(dc1);
            dataTable.Columns.Add(dc2);

            JSONclasses = new JsonLoader().loadCourseList(filePath);
            for (var i = 0; i < JSONclasses.Count; i++)
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = JSONclasses[i].courseNumber;
                dr[1] = JSONclasses[i].courseTitle;

                dataTable.Rows.Add(dr);
            }
            dgv = new DataGridView();
            dgv.AutoGenerateColumns = false;
            int numberOfColumns = 2;
            dgv.ColumnCount = numberOfColumns;
            dgv.Columns[0].DataPropertyName = "Course Number";
            dgv.Columns[1].DataPropertyName = "Course Title";
            dgv.DataSource = dataTable;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Edit Class?";
            buttonColumn.CellTemplate = new DataGridViewButtonCell();
            dgv.Columns.Add(buttonColumn);
            dgv.CellMouseClick += new DataGridViewCellMouseEventHandler(cellClick);
            for (int i = 0; i < numberOfColumns; i++)
            {
                dgv.Columns[i].ReadOnly = true;
            }

            editSelectorGrid.Child = dgv;
        }

        private void cellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                Edit window = new Edit(filePath, e.RowIndex);
                this.NavigationService.Navigate(window);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Editor window = new Editor();
            this.NavigationService.Navigate(window);
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
