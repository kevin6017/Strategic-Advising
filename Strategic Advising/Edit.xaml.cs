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
        public Edit(List<Course> passedCourseList, int passedClassIndex, YAMLLoader passedLoader)
        {
            InitializeComponent();
            courseList = passedCourseList;
            classIndex = passedClassIndex;
            this.loader = passedLoader;
        }

        int classIndex;
        List<Course> courseList;
        List<Course> prereqs = new List<Course>();
        private string oldCourseNumber;
        private YAMLLoader loader;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            courseNumberInput.Text = courseList[classIndex].courseNumber; //popular online opinion says we should use bindings and dependencies to do this instead of doing directly
            courseTitleInput.Text = courseList[classIndex].courseTitle;
            fallInput.IsChecked = courseList[classIndex].fall;
            springInput.IsChecked = courseList[classIndex].spring;
            foreach (ComboBoxItem item in creditHourInput.Items)
            {
                if (item.Content as String == courseList[classIndex].creditHours.ToString())
                {
                    creditHourInput.SelectedItem = item;
                    break;
                }
            }

            if (courseList[classIndex].prerequisites != null)
            {
                prereqs = courseList[classIndex].prerequisites;
                for (int i = 0; i < prereqs.Count; i++)
                {
                    if (i == prereqs.Count - 1)
                    {
                        listOfPrereqs.Text += prereqs[i].courseNumber;
                    }
                    else
                    {
                        listOfPrereqs.Text += prereqs[i].courseNumber + ", ";
                    }
                }
            }
            oldCourseNumber = courseList[classIndex].courseNumber;
        }

        private void Edit_Course_Button(object sender, RoutedEventArgs e)
        {
            courseList[classIndex].courseNumber = courseNumberInput.Text;
            courseList[classIndex].courseTitle = courseTitleInput.Text;
            courseList[classIndex].creditHours = Int32.Parse(((ComboBoxItem)creditHourInput.SelectedItem).Content.ToString());
            courseList[classIndex].fall = (bool)fallInput.IsChecked;
            courseList[classIndex].spring = (bool)springInput.IsChecked;
            courseList[classIndex].prerequisites = prereqs;
            //this.loader.changeCourseInfo(oldCourseNumber, courseList[classIndex]);
            Editor window = new Editor(this.loader);
            this.NavigationService.Navigate(window);
        }

        private void Prereqs_Selector_Button(object sender, RoutedEventArgs e)
        {
            ClassSelectorWindow csWindow = new ClassSelectorWindow(this.loader);
            bool? dialogResult = csWindow.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    if (prereqs == null)
                    {
                        prereqs = csWindow.selectedCourses();
                    }
                    else
                    {
                        prereqs.AddRange(csWindow.selectedCourses());
                    }
                    break;
                case false:
                    break;
                default:
                    break;
            }
            if (prereqs != null)
            {
                listOfPrereqs.Text = "";
                for (int i = 0; i < prereqs.Count; i++)
                {
                    if (i == prereqs.Count - 1)
                    {
                        listOfPrereqs.Text += prereqs[i].courseNumber;
                    }
                    else
                    {
                        listOfPrereqs.Text += prereqs[i].courseNumber + ", ";
                    }
                }
            }
        }

        private void Prereq_Clear_Button(object sender, RoutedEventArgs e)
        {
            prereqs.Clear();
            listOfPrereqs.Text = "";
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            Editor window = new Editor();
            this.NavigationService.Navigate(window);
        }
    }
}
