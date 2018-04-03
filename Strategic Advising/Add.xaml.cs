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
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add(int passedIndex)
        {
            InitializeComponent();
            this.index = passedIndex;
            loader = new YAMLLoader();
        }

        YAMLLoader loader;
        List<Curriculum> curric;
        List<Course> prereqs;
        private int index;

        private void Add_Class_Button(object sender, RoutedEventArgs e)
        {
            Course newClass = new Course();
            newClass.courseNumber = courseNumBox.Text;
            newClass.courseTitle = courseTitleBox.Text;
            newClass.creditHours = Int32.Parse(((ComboBoxItem)creditHourInput.SelectedItem).Content.ToString());
            newClass.fall = (bool)fallInput.IsChecked;
            newClass.spring = (bool)springInput.IsChecked;
            //newClass.prerequisites = prereqs;
            //must instantiate a new list of type course for the prerquisites. Cannot leave unassigned.
            newClass.prerequisites = new List<Course>();
            loader.addCourseToCurriculum(index, newClass, prereqs);
            loader.serializeFile();
            

            //Editor window = new Editor(); 
            //this.NavigationService.Navigate(window);
        }

        private void Prereqs_Selector_Button(object sender, RoutedEventArgs e)
        {
            ClassSelectorWindow csWindow = new ClassSelectorWindow();
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
            for (int i = 1; i < prereqs.Count; i++)
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
