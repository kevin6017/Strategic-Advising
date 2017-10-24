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

namespace Strategic_Advising
{
    /// <summary>
    /// Interaction logic for TentativeSchedule.xaml
    /// </summary>
    public partial class TentativeSchedule : Page
    {
        public TentativeSchedule()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid dataGrid = new DataGrid();
            DataTable dt = new DataTable("sampleTable");
            DataColumn dc1 = new DataColumn("Course Number", typeof(string));
            dt.Columns.Add(dc1);
            DataRow dr = dt.NewRow();
            dr[0] = "CS222";
            dt.Rows.Add(dr);
            dataGrid.ItemsSource = dt.DefaultView;
            stackPanel.Children.Add(dataGrid);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataGridTest window = new DataGridTest();
            this.NavigationService.Navigate(window);
        }
    }
}
