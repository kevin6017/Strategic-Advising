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
using Newtonsoft.Json;
using System.Reflection;
using System.IO;

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
            /*System.Windows.Controls.DataGrid dataGrid = new DataGrid();
            DataTable dt = new DataTable("sampleTable");
            DataColumn dc1 = new DataColumn("Course Number", typeof(string));
            dt.Columns.Add(dc1);
            DataRow dr = dt.NewRow();
            dr[0] = "CS222";
            dt.Rows.Add(dr);
            dataGrid.ItemsSource = dt.DefaultView;
            stackPanel.Children.Add(dataGrid);*/

            var jsonSchedule = loadSchedule();
            for (var i = 0; i<jsonSchedule.Count; i++)
            {
                System.Windows.Controls.DataGrid dg = new DataGrid();
                DataTable dt = new DataTable("semesterTable");
                dt.Columns.Add(new DataColumn("Course Title", typeof(string)));
                /*dr[0] = string.Join(" ", jsonSchedule[i].classes);
                dt.Rows.Add(dr);*/
                for (var j = 0; j<jsonSchedule[i].classes.Count(); j++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = jsonSchedule[i].classes[j];
                    dt.Rows.Add(dr);
                }
                dg.ItemsSource = dt.DefaultView;
                stackPanel.Children.Add(dg);
            }
            

            
        }

        private List<Semester> loadSchedule()
        {
            Assembly assemblly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assemblly.GetManifestResourceStream("Strategic_Advising.res.SampleSchedule.json"));
            string json = reader.ReadToEnd();
            var jsonObject = JsonConvert.DeserializeObject<List<Semester>>(json);
            return jsonObject;
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
