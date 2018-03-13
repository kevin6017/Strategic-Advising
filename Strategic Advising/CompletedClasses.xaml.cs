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
    public partial class CompletedClasses : Page
    {
        public CompletedClasses(int major, int core, bool fall, int semesters)
        {
            InitializeComponent();
            majorIndex = major;
            coreIndex = core;
            isFall = fall;
            numSemesters = semesters;
        }

        DataGridView dgv;
        DataTable dataTable;
        int majorIndex;
        int coreIndex;
        bool isFall;
        int numSemesters;



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dataTable = new DataTable("sampleTable");
            
            //DataColumn dc1 = new DataColumn("Course Number", typeof(string));
            //DataColumn dc2 = new DataColumn("Course Title", typeof(string));
            //DataColumn dc3 = new DataColumn("Credit Hours", typeof(int));
            //DataColumn dc4 = new DataColumn("Spring Class", typeof(bool));
            //DataColumn dc5 = new DataColumn("Fall Class", typeof(bool));
            //DataColumn dc6 = new DataColumn("Prerequisites", typeof(string));

            //dc4.ReadOnly = true;
            //dc5.ReadOnly = true;

            //dataTable.Columns.Add(dc1);
            //dataTable.Columns.Add(dc2);
            //dataTable.Columns.Add(dc3);
            //dataTable.Columns.Add(dc4);
            //dataTable.Columns.Add(dc5);
            //dataTable.Columns.Add(dc6);
            
            
            //var JSONclasses = new JsonLoader().loadCourseList("Strategic_Advising.res.HonorsCoreClasses.json");
            YAMLLoader loader = new YAMLLoader();
            List<Course> courseList = loader.getCurriculum(coreIndex);
            courseList.AddRange(loader.getCurriculum(majorIndex));
            dgv = new DataGridView();
            dgv.DataSource = courseList;
            dgv.ReadOnly = true;
            sampleGrid.Child = dgv;
            
            //foreach(Course course in courseList) //theres an extra row being created here? (Issue #2)
            //{
            //    DataRow dr = dataTable.NewRow();
            //    dr[0] = JSONclasses[i].courseNumber;
            //    dr[1] = JSONclasses[i].courseTitle;
            //    dr[2] = JSONclasses[i].creditHours;
            //    dr[3] = JSONclasses[i].fall;
            //    dr[4] = JSONclasses[i].spring;
            //    dr[5] = string.Join(", ", JSONclasses[i].prerequisites);
                
            //    dataTable.Rows.Add(dr);
                
            //}
            //dgv = new DataGridView();
            //dgv.AutoGenerateColumns = false;
            //dgv.ColumnCount = 6;
            //dgv.Columns[0].DataPropertyName = "Course Number";
            //dgv.Columns[1].DataPropertyName = "Course Title";
            //dgv.Columns[2].DataPropertyName = "Credit Hours";
            //dgv.Columns[3].DataPropertyName = "Spring Class";
            //dgv.Columns[4].DataPropertyName = "Fall Class";
            //dgv.Columns[5].DataPropertyName = "Prerequisites";
            //dgv.DataSource = dataTable;
            //DataGridViewCheckBoxColumn ckCol = new DataGridViewCheckBoxColumn();
            //ckCol.HeaderText = "Check Column";
            //ckCol.CellTemplate = new DataGridViewCheckBoxCell();
            //ckCol.ReadOnly = false;
            //dgv.Columns.Add(ckCol);
            //for (int i = 0; i<6; i++)
            //{
            //    dgv.Columns[i].ReadOnly = true;
            //}
            
            //sampleGrid.Child = dgv;

            

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Course> completedCourses = new List<Course>();
            foreach(DataGridViewRow row in dgv.SelectedRows)
            {
                var item = row.DataBoundItem as Course;
                completedCourses.Add(item);
            }
            Scheduler scheduler = new Scheduler(completedCourses, numSemesters, isFall, coreIndex, majorIndex);
            List<Semester> listOfSemesters = scheduler.getSemesterList();
            //List<string> completedClasses = new List<string>();
            //foreach (DataGridViewRow row in dgv.Rows)
            //{
                
            //    DataGridViewCheckBoxCell currentCell = new DataGridViewCheckBoxCell();
            //    currentCell = (DataGridViewCheckBoxCell)row.Cells[6];
            //    if (currentCell.Value != null && (bool)currentCell.Value == true)
            //    {
            //        completedClasses.Add((string)row.Cells[0].Value);
            //    }
            //}
            //create shceduler here 
            TentativeSchedule window = new TentativeSchedule(listOfSemesters, this);
            this.NavigationService.Navigate(window);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }
    }
}
