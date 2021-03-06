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
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Page
    {
        public Editor()
        {
            InitializeComponent();
            this.loader = new YAMLLoader();
            populateMajorSelectBox();
            
        }

        public Editor(YAMLLoader passedLoader)
        {
            InitializeComponent();
            this.loader = passedLoader;
            populateMajorSelectBox();
        }

       private YAMLLoader loader;

        private void populateMajorSelectBox()
        {
            List<Curriculum> curricList = this.loader.getMasterList();
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
            this.loader.serializeFile();
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }

        private List<Course> courseList;

        private void addButtonClick(object sender, RoutedEventArgs e)
        {
            //getSelectedClass()
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            int curricIndex = Int32.Parse(temp.Tag.ToString());
            Add window = new Add(this.loader, curricIndex);
            this.NavigationService.Navigate(window);
        }

        private void editButtonClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            int curricIndex = Int32.Parse(temp.Tag.ToString());
            EditSelector window = new EditSelector(this.loader, curricIndex);
            this.NavigationService.Navigate(window);
        }

        private void Button_ClickRemove(object sender, RoutedEventArgs e)
        {
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            int curricIndex = Int32.Parse(temp.Tag.ToString());
            Remove window = new Remove(curricIndex, this.loader);
            this.NavigationService.Navigate(window);
        }

        //private void getSelectedClass()
        //{
        //    ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
        //    int curricIndex = Int32.Parse(temp.Tag.ToString());
        //    courseList = new YAMLLoader().getCurriculum(curricIndex);
        //}

    }
}
