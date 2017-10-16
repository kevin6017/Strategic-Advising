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
using System.Data;
using System.Data.SqlClient;

namespace Strategic_Advising
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class DataGridTest : Page
    {
        public DataGridTest()
        {
            InitializeComponent();
        }

        DataTable dataTable;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable = new DataTable("sampleTable");
            DataColumn dc1 = new DataColumn("CourseID", typeof(string));
            DataColumn dc2 = new DataColumn("Course Num", typeof(string));
            dataTable.Columns.Add(dc1);
            dataTable.Columns.Add(dc2);
            sampleGrid.ItemsSource = dataTable.DefaultView;
            DataRow dr = dataTable.NewRow();
            dr[0] = "CS120";
            dr[1] = "CS121";
            dataTable.Rows.Add(dr);
            sampleGrid.ItemsSource = dataTable.DefaultView;

        }
        
    }
}
