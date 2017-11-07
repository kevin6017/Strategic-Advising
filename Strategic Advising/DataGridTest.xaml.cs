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
using System.Reflection;
using System.Windows.Forms;

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

        DataGridView dgv;
        DataTable dataTable;
        Assembly _assembly;
        StreamReader _textStreamReader;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable = new DataTable("sampleTable");
            
            DataColumn dc1 = new DataColumn("Course Number", typeof(string));
            DataColumn dc2 = new DataColumn("Course Title", typeof(string));
            DataColumn dc3 = new DataColumn("Credit Hours", typeof(int));
            DataColumn dc4 = new DataColumn("Spring Class", typeof(bool));
            DataColumn dc5 = new DataColumn("Fall Class", typeof(bool));
            DataColumn dc6 = new DataColumn("Prerequisites", typeof(string));

            dc4.ReadOnly = true;
            dc5.ReadOnly = true;

            dataTable.Columns.Add(dc1);
            dataTable.Columns.Add(dc2);
            dataTable.Columns.Add(dc3);
            dataTable.Columns.Add(dc4);
            dataTable.Columns.Add(dc5);
            dataTable.Columns.Add(dc6);
            
            
            var JSONclasses = new JsonLoader().loadCourseList("Strategic_Advising.res.HonorsCoreClasses.json");
            for (var i=0; i<JSONclasses.Count; i++) //theres an extra row being created here? (Issue #2)
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = JSONclasses[i].courseNumber;
                dr[1] = JSONclasses[i].courseTitle;
                dr[2] = JSONclasses[i].creditHours;
                dr[3] = JSONclasses[i].fall; //these two are clickable (Issue #1)
                dr[4] = JSONclasses[i].spring; //not sure how to lock them
                dr[5] = string.Join(", ", JSONclasses[i].prerequisites);
                
                dataTable.Rows.Add(dr);
                
            }
            dgv = new DataGridView();
            dgv.AutoGenerateColumns = false;
            dgv.ColumnCount = 6;
            dgv.Columns[0].DataPropertyName = "Course Number";
            dgv.Columns[1].DataPropertyName = "Course Title";
            dgv.Columns[2].DataPropertyName = "Credit Hours";
            dgv.Columns[3].DataPropertyName = "Spring Class";
            dgv.Columns[4].DataPropertyName = "Fall Class";
            dgv.Columns[5].DataPropertyName = "Prerequisites";
            dgv.DataSource = dataTable;
            DataGridViewCheckBoxColumn ckCol = new DataGridViewCheckBoxColumn();
            ckCol.HeaderText = "Check Column";
            ckCol.CellTemplate = new DataGridViewCheckBoxCell();
            ckCol.ReadOnly = false;
            dgv.Columns.Add(ckCol);
            for (int i = 0; i<6; i++)
            {
                dgv.Columns[i].ReadOnly = true;
            }
            
            sampleGrid.Child = dgv;

        }

        /*private List<Course> loadCourseList()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("Strategic_Advising.res.HonorsCoreClasses.json"));
            string json = _textStreamReader.ReadToEnd();
            var jsonObject = JsonConvert.DeserializeObject<List<Course>>(json);
            return jsonObject;
            
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> completedClasses = new List<string>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                
                DataGridViewCheckBoxCell currentCell = new DataGridViewCheckBoxCell();
                currentCell = (DataGridViewCheckBoxCell)row.Cells[6];
                if (currentCell.Value != null && (bool)currentCell.Value == true)
                {
                    completedClasses.Add((string)row.Cells[0].Value);
                }
            }

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
