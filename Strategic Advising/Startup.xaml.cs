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
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Page
    {
        public Startup()
        {
            InitializeComponent();
        }

        public void onSubmit(object sender, RoutedEventArgs e)
        {
            int majorIndex = -1;
            int coreIndex = 0;
            if (CS.IsChecked == true)
            {
                majorIndex = 3;
            }
            else
            {
                majorIndex = 2;
            }
            if(chkHonors.IsChecked == true)
            {
                coreIndex = 1;
            }
            bool isFall = (bool)Fall.IsChecked;
            int numSemesters = Int32.Parse(((ComboBoxItem)ddlSemesters.SelectedItem).Content.ToString());
            CompletedClasses window = new CompletedClasses(majorIndex, coreIndex, isFall, numSemesters);
            this.NavigationService.Navigate(window);
        }

        private void editorClick(object sender, RoutedEventArgs e)
        {
            Editor window = new Editor();
            this.NavigationService.Navigate(window);
        }
    }
}
