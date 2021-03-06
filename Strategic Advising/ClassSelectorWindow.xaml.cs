﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Strategic_Advising
{
    /// <summary>
    /// Interaction logic for ClassSelectorWindow.xaml
    /// </summary>
    public partial class ClassSelectorWindow : Window
    {
        private YAMLLoader loader;
        private DataGridView dgv;
        private List<Course> coursesToAdd;

        public ClassSelectorWindow(YAMLLoader passedLoader)
        {
            InitializeComponent();
            this.loader = passedLoader;
            populateMajorSelectBox();
        }

        private void populateMajorSelectBox()
        {
            List<Curriculum> curricList = loader.getMasterList();
            for (int i = 1; i < curricList.Count; i++)
            {
                ListBoxItem li = new ListBoxItem();
                li.Content = curricList[i].name;
                li.Tag = i;
                if (i == 3)
                {
                    li.IsSelected = true;
                }
                majorSelect.Items.Add(li);
            }
        }

        private void loadButtonClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            int curricIndex = Int32.Parse(temp.Tag.ToString());
            List<Course> courseList = this.loader.getCurriculum(curricIndex);
            dgv = new DataGridView();
            dgv.DataSource = courseList;
            dgv.ReadOnly = true;
            sampleGrid.Child = dgv;
        }

        private void selectButtonClick(object sender, RoutedEventArgs e)
        {
            coursesToAdd = new List<Course>();
            foreach(DataGridViewRow row in dgv.SelectedRows)
            {
                Course course = row.DataBoundItem as Course;
                coursesToAdd.Add(course);
            }
            DialogResult = true;
            this.Close();
        }

        public List<Course> selectedCourses()
        {
            return coursesToAdd;
        }
    }
}
