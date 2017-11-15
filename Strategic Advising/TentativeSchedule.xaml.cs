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
using System.Windows.Forms;

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
            
            FlowLayoutPanel fallpanel = new FlowLayoutPanel();
            FlowLayoutPanel springpanel = new FlowLayoutPanel();
            fallpanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            springpanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;

            var jsonSchedule = new JsonLoader().loadScheduleList("Strategic_Advising.res.SampleSchedule.json");

            bool isFall = true;

            for (var i = 0; i < jsonSchedule.Count; i++)
            {
                DataGridView dgv = new DataGridView();
                dgv.ColumnCount = 0;
                DataTable dt = new DataTable("semesterTable");
                dt.Columns.Add(new DataColumn("Course Title", typeof(string)));
                for (var j = 0; j < jsonSchedule[i].classes.Count(); j++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = jsonSchedule[i].classes[j];
                    dt.Rows.Add(dr);
                }
                dgv.Width = 250;
                dgv.DataSource = dt;
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.HeaderText = "Action Button";
                btnCol.Name = "button";
                btnCol.Text = "Action";
                btnCol.UseColumnTextForButtonValue = true;
                dgv.Columns.Add(btnCol);
                dgv.CellMouseClick += new DataGridViewCellMouseEventHandler(cellClick);
                
                if (isFall)
                {
                    fallpanel.Controls.Add(dgv);
                }
                else
                {
                    springpanel.Controls.Add(dgv);
                }

                isFall = !isFall;
            }
            fallForm.Child = fallpanel;
            springForm.Child = springpanel;


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

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == fallView)
            {
                springView.ScrollToHorizontalOffset(e.HorizontalOffset);
                springView.ScrollToVerticalOffset(e.VerticalOffset);
            }
            else
            {
                fallView.ScrollToHorizontalOffset(e.HorizontalOffset);
                fallView.ScrollToVerticalOffset(e.VerticalOffset);
            }
        }

        private void cellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (sender as DataGridView);
            if(e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewCell cell = dgv[e.ColumnIndex, e.RowIndex];
                if (!cell.Selected)
                {
                    cell.DataGridView.ClearSelection();
                    cell.DataGridView.CurrentCell = cell;
                    cell.Selected = true;
                }
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
                menu.MenuItems.Add(new System.Windows.Forms.MenuItem("Move"));
                menu.MenuItems.Add(new System.Windows.Forms.MenuItem("Add a class"));
                menu.MenuItems.Add(new System.Windows.Forms.MenuItem("Remove class"));

                int hitTest = dgv.HitTest(e.X, e.Y).RowIndex;
                int hittestY = dgv.HitTest(e.X, e.Y).ColumnIndex;
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                menu.Show(dgv, new System.Drawing.Point(e.X+150, e.Y+25));
            }
        }
    }
}
