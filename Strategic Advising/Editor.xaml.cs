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
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Page
    {
        public Editor()
        {
            InitializeComponent();
            populateMajorSelectBox();
        }

        private void populateMajorSelectBox()
        {
            YAMLLoader loader = new YAMLLoader();
            List<Curriculum> curricList = loader.getMasterList();
            for (int i = 1; i < curricList.Count; i++)
            {
                ListBoxItem li = new ListBoxItem();
                li.Content = curricList[i].name;
                li.Tag = i;
                if (i == 1)
                {
                    li.IsSelected = true;
                }
                majorSelect.Items.Add(li);
            }
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        List<Course> courseList;

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            //getSelectedClass();
            YAMLLoader loader = new YAMLLoader();
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            int curricIndex = Int32.Parse(temp.Tag.ToString());
            Add window = new Add(loader.getMasterList(), curricIndex);
            this.NavigationService.Navigate(window);
        }

        private void Button_ClickEdit(object sender, RoutedEventArgs e)
        {
            getSelectedClass();
            EditSelector window = new EditSelector(courseList);
            this.NavigationService.Navigate(window);
        }

        private void Button_ClickRemove(object sender, RoutedEventArgs e)
        {
            getSelectedClass();
            Remove window = new Remove(courseList);
            this.NavigationService.Navigate(window);
        }

        private void getSelectedClass()
        {
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            int curricIndex = Int32.Parse(temp.Tag.ToString());
            courseList = new YAMLLoader().getCurriculum(curricIndex);
        }

    }
}
