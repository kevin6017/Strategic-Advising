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
        public Edit(string passedFilePath, int passedClassIndex)
        {
            InitializeComponent();
            filePath = passedFilePath;
            classIndex = passedClassIndex;
        }

        string filePath;
        int classIndex;
        List<Course> JSONclasses;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            JSONclasses = new JsonLoader().loadCourseList(filePath);
            courseNumberInput.Text = JSONclasses[classIndex].courseNumber; //popular online opinion says we should use bindings and dependencies to do this instead of doing directly
            courseTitleInput.Text = JSONclasses[classIndex].courseTitle;
            creditHoursInput.Text = JSONclasses[classIndex].creditHours;
            fallInput.IsChecked = JSONclasses[classIndex].fall;
            springInput.IsChecked = JSONclasses[classIndex].spring;
            prerequisitesInput.Text = string.Join(", ", JSONclasses[classIndex].prerequisites); ; //will need a better way to do this, need to preserve string[]
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditSelector window = new EditSelector(filePath);
            this.NavigationService.Navigate(window);
        }
    }
}
