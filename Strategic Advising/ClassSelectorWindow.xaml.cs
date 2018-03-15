using System;
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
        private YAMLLoader loader = new YAMLLoader();
        //private List<Curriculum> curric = new List<Curriculum>();

        public ClassSelectorWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem temp = (ListBoxItem)majorSelect.SelectedItem;
            int curricIndex = Int32.Parse((string)temp.Tag);
            List<Course> courseList = this.loader.getCurriculum(curricIndex);
            DataGridView dgv = new DataGridView();
            dgv.DataSource = courseList;
            dgv.ReadOnly = true;
            sampleGrid.Child = dgv;
        }
    }
}
