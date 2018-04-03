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
    /// Interaction logic for Remove.xaml
    /// </summary>
    public partial class Remove : Page
    {
        public Remove(List<Course> passedCourseList)
        {
            InitializeComponent();
            courseList = passedCourseList;
        }
        List<Course> courseList;
        DataGridView dgv;
        DataTable dataTable;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable = new DataTable("removeTable");
            DataColumn dc1 = new DataColumn("Course Number", typeof(string));
            DataColumn dc2 = new DataColumn("Course Title", typeof(string));
            dataTable.Columns.Add(dc1);
            dataTable.Columns.Add(dc2);

            for (var i = 0; i < courseList.Count; i++)
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = courseList[i].courseNumber;
                dr[1] = courseList[i].courseTitle;

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
            buttonColumn.HeaderText = "Remove Class?";
            buttonColumn.CellTemplate = new DataGridViewButtonCell();
            dgv.Columns.Add(buttonColumn);
            dgv.CellMouseClick += new DataGridViewCellMouseEventHandler(cellClick);
            for (int i = 0; i < numberOfColumns; i++)
            {
                dgv.Columns[i].ReadOnly = true;
            }

            removeGrid.Child = dgv;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Editor window = new Editor();
            this.NavigationService.Navigate(window);

        }

        private void cellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to remove this class?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Course courseToRemove = courseList[e.RowIndex];
                    testLabel.Content = courseToRemove.courseNumber.ToString();
                    //Remove stuff and go back to editor page
                }
            }
        }

        

        
    }
}
