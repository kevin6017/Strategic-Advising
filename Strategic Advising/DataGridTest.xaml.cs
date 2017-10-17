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
            DataColumn dc1 = new DataColumn("FName", typeof(string));
            DataColumn dc2 = new DataColumn("LName", typeof(string));
            dataTable.Columns.Add(dc1);
            dataTable.Columns.Add(dc2);
            sampleGrid.ItemsSource = dataTable.DefaultView;
            var jsonObject = loadJson();
            for (var i=0; i<jsonObject.Count; i++)
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = jsonObject[i].fname;
                dr[1] = jsonObject[i].lname;
                dataTable.Rows.Add(dr);
                sampleGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private List<person> loadJson()
        {
            string json = System.IO.File.ReadAllText(path: "../../res/tsconfig1.json" );
            var jsonObject = JsonConvert.DeserializeObject<List<person>>(json);
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

        
        
    }
}
