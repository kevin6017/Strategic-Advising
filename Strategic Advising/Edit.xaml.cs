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

namespace Strategic_Advising
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        public Edit(List<Course> passedCourseList, int passedClassIndex)
        {
            InitializeComponent();
            courseList = passedCourseList;
            classIndex = passedClassIndex;
        }

        int classIndex;
        List<Course> courseList;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            courseNumberInput.Text = courseList[classIndex].courseNumber; //popular online opinion says we should use bindings and dependencies to do this instead of doing directly
            courseTitleInput.Text = courseList[classIndex].courseTitle;
            creditHoursInput.Text = courseList[classIndex].creditHours.ToString();
            fallInput.IsChecked = courseList[classIndex].fall;
            springInput.IsChecked = courseList[classIndex].spring;
            prerequisitesInput.Text = string.Join(", ", courseList[classIndex].prerequisites); ; //will need a better way to do this, need to preserve string[]
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditSelector window = new EditSelector(courseList);
            this.NavigationService.Navigate(window);
        }
    }
}
