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
using Microsoft.Win32;

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
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.DefaultExt = ".eyaml";
            //if (ofd.ShowDialog() == true)
            //{
            //    string filename = ofd.FileName;

            //}
            populateMajorSelectBox();
            
        }

        private void populateMajorSelectBox()
        {
            YAMLLoader loader = new YAMLLoader();
            List<Curriculum> curricList = loader.getMasterList();
            for(int i=3; i<curricList.Count; i++)
            {
                ListBoxItem li = new ListBoxItem();
                li.Content = curricList[i].name;
                li.Tag = i;
                if (i == 3)
                {
                    li.IsSelected = true;
                }
                lbxMajors.Items.Add(li);
            }
        }

        public void onSubmit(object sender, RoutedEventArgs e)
        {
            int majorIndex = -1;
            int coreIndex = 1;
            if (chkHonors.IsChecked == true)
            {
                coreIndex = 2;
            }
            majorIndex = getMajorIndex();
            bool isFall = (bool)Fall.IsChecked;
            int numSemesters = Int32.Parse(((ComboBoxItem)ddlSemesters.SelectedItem).Content.ToString());
            int maxCredits = Int32.Parse(((ComboBoxItem)ddlMaxCredits.SelectedItem).Content.ToString());
            int minCredits = Int32.Parse(((ComboBoxItem)ddlMinCredits.SelectedItem).Content.ToString());
            CompletedClasses window = new CompletedClasses(majorIndex, coreIndex, isFall, numSemesters, maxCredits, minCredits);
            this.NavigationService.Navigate(window);
        }

        private int getMajorIndex()
        {
            ListBoxItem li = (ListBoxItem)lbxMajors.SelectedItem;
            int index = Int32.Parse(li.Tag.ToString());
            return index;
        }

        private void editorClick(object sender, RoutedEventArgs e)
        {
            Editor window = new Editor();
            this.NavigationService.Navigate(window);
        }
    }
}
