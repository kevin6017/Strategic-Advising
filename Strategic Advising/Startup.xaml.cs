﻿using System;
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

        private void onSubmit(object sender, RoutedEventArgs e)
        {
            //CompletedClasses window = new CompletedClasses();
            //this.NavigationService.Navigate(window);
            DataGridTest test = new DataGridTest();
            this.NavigationService.Navigate(test);
        }

        private void editorClick(object sender, RoutedEventArgs e)
        {
            Editor window = new Editor();
            this.NavigationService.Navigate(window);
        }
    }
}