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
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        string selectedMajorName;
        string selectedMajorJSONFile;

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            getSelectedClass();
            Add window = new Add();
            this.NavigationService.Navigate(window);
        }

        private void Button_ClickEdit(object sender, RoutedEventArgs e)
        {
            getSelectedClass();
            EditSelector window = new EditSelector();
            this.NavigationService.Navigate(window);
        }

        private void Button_ClickRemove(object sender, RoutedEventArgs e)
        {
            getSelectedClass();
            Remove window = new Remove(selectedMajorJSONFile);
            this.NavigationService.Navigate(window);
        }

        private void getSelectedClass()
        {
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            selectedMajorJSONFile = temp.Name + ".json"; //probably a more official way to do this besides using the name but oh well
            selectedMajorName = (string) temp.Content;
            // noClassesMessage = System.Windows.MessageBox.Show(selectedMajorJSONFile + "\r\n" + selectedMajorName, "Confirmation", MessageBoxButton.OK);

        }
    }
}
