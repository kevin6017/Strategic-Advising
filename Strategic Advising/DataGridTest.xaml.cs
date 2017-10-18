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
using System.IO;
using Newtonsoft.Json;

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
            DataColumn dc1 = new DataColumn("Course Number", typeof(string));
            DataColumn dc2 = new DataColumn("Course Title", typeof(string));
            DataColumn dc3 = new DataColumn("Credit Hours", typeof(int));
            DataColumn dc4 = new DataColumn("Spring Class", typeof(bool));
            DataColumn dc5 = new DataColumn("Fall Class", typeof(bool));
            DataColumn dc6 = new DataColumn("Prerequisites", typeof(string[]));
            dataTable.Columns.Add(dc1);
            dataTable.Columns.Add(dc2);
            dataTable.Columns.Add(dc3);
            dataTable.Columns.Add(dc4);
            dataTable.Columns.Add(dc5);
            dataTable.Columns.Add(dc6);
            sampleGrid.ItemsSource = dataTable.DefaultView;
            var JSONclasses = loadCourseList();
            for (var i=0; i<JSONclasses.Count; i++)
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = JSONclasses[i].courseNumber;
                dr[1] = JSONclasses[i].courseTitle;
                dr[2] = JSONclasses[i].creditHours;
                dr[3] = JSONclasses[i].fall;
                dr[4] = JSONclasses[i].spring;
                dr[5] = JSONclasses[i].prerequisites;
                dataTable.Rows.Add(dr);
                sampleGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private List<Course> loadCourseList()
        {
            string json = System.IO.File.ReadAllText(path: "../../res/CSclasses.json" );
            var jsonObject = JsonConvert.DeserializeObject<List<Course>>(json);
            return jsonObject;
            
            
        }

        class person
        {
            public string fname
            {
                get; set;
            }

            public string lname
            {
                get; set;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TentativeSchedule window = new TentativeSchedule();
            this.NavigationService.Navigate(window);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }
    }
}
