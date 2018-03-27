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

    public partial class CompletedClasses : Page
    {
        public CompletedClasses(int major, int core, bool fall, int semesters, int maxCredits, int minCredits)
        {
            InitializeComponent();
            majorIndex = major;
            coreIndex = core;
            isFall = fall;
            numSemesters = semesters;
            this.maxCredits = maxCredits;
            this.minCredits = minCredits;
        }

        DataGridView dgv;
        DataTable dataTable;
        int majorIndex;
        int coreIndex;
        bool isFall;
        int numSemesters;
        int maxCredits;
        int minCredits;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            YAMLLoader loader = new YAMLLoader();
            List<Course> courseList = loader.getCurriculum(coreIndex);
            courseList.AddRange(loader.getCurriculum(majorIndex));
            dgv = new DataGridView();
            dgv.DataSource = courseList;
            dgv.ReadOnly = true;
            sampleGrid.Child = dgv;           
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            HashSet<Course> completedCoursesSet = new HashSet<Course>();
            foreach(DataGridViewRow row in dgv.SelectedRows)
            {
                var item = row.DataBoundItem as Course;
                completedCoursesSet = getAllPrereqs(item, completedCoursesSet);
            }
            List<Course> completedCourses = completedCoursesSet.ToList<Course>();
            Scheduler scheduler = new Scheduler(completedCourses, numSemesters, isFall, coreIndex, majorIndex, maxCredits, minCredits);
            List<Semester> listOfSemesters = scheduler.getSemesterList();
           
            TentativeSchedule window = new TentativeSchedule(listOfSemesters, this);
            this.NavigationService.Navigate(window);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }

        private HashSet<Course> getAllPrereqs(Course course, HashSet<Course> courseSet)
        {
            if (course.prerequisites != null)
            {
                foreach (Course prereq in course.prerequisites)
                {
                    courseSet.Add(prereq);
                    courseSet = getAllPrereqs(prereq, courseSet);
                }
                return courseSet;
            }
            else
            {
                return courseSet;
            }
        }
    }
}
