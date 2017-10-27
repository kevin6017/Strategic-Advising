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
using System.Data;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;

namespace Strategic_Advising
{
    /// <summary>
    /// Interaction logic for TentativeSchedule.xaml
    /// </summary>
    public partial class TentativeSchedule : Page
    {
        public TentativeSchedule()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid bigDG = new DataGrid();
            DataTable bigDataTable = new DataTable("massiveTable");
            bigDataTable.Columns.Add(new DataColumn(("Fall"), typeof(DataGrid)));
            bigDataTable.Columns.Add(new DataColumn(("Spring"), typeof(DataGrid)));
            List<DataGrid> dataGridList = new List<DataGrid>();

            var jsonSchedule = loadSchedule();

            bool isFall = true;

            for (var i = 0; i<jsonSchedule.Count; i++)
            {
                System.Windows.Controls.DataGrid dg = new DataGrid();
                DataTable dt = new DataTable("semesterTable");
                dt.Columns.Add(new DataColumn("Course Title", typeof(string)));
                for (var j = 0; j<jsonSchedule[i].classes.Count(); j++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = jsonSchedule[i].classes[j];
                    dt.Rows.Add(dr);
                }
                dg.ItemsSource = dt.DefaultView;
                //dataGridList.Add(dg);
                if (isFall)
                {
                    stackPanelFall.Children.Add(dg);
                }
                else
                {
                    stackPanelSpring.Children.Add(dg);
                }
                isFall = !isFall;
            }

            /*while (dataGridList.Count > 0)
            {
                DataRow dr = bigDataTable.NewRow();
                if (dataGridList.Count != 1) { // if there is not just one semester left                    
                    dr[0] = dataGridList.ElementAt(0);
                    dataGridList.Remove(dataGridList[0]);
                    dr[1] = dataGridList[0];
                    dataGridList.Remove(dataGridList[0]);                    
                }
                else
                {
                    dr[0] = dataGridList[0];
                    dataGridList.Remove(dataGridList[0]);
                }
                bigDataTable.Rows.Add(dr);
            }
            bigDG.ItemsSource = bigDataTable.DefaultView;
            stackPanel.Children.Add(bigDG);*/
        }

        private List<Semester> loadSchedule()
        {
            Assembly assemblly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assemblly.GetManifestResourceStream("Strategic_Advising.res.SampleSchedule.json"));
            string json = reader.ReadToEnd();
            var jsonObject = JsonConvert.DeserializeObject<List<Semester>>(json);
            return jsonObject;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Startup window = new Startup();
            this.NavigationService.Navigate(window);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataGridTest window = new DataGridTest();
            this.NavigationService.Navigate(window);
        }
    }
}
