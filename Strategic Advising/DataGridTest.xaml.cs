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
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        DataTable dataTable;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable = new DataTable("sampleTable");
            DataColumn dc1 = new DataColumn("CourseID", typeof(string));
            dataTable.Columns.Add(dc1);
            sampleGrid.ItemsSource = dataTable.DefaultView;
        }
        
    }
}
