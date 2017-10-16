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
    }
}
