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
using System.ComponentModel;

namespace Strategic_Advising
{
    /// <summary>
    /// Interaction logic for TentativeSchedule.xaml
    /// </summary>
    public partial class TentativeSchedule : Page
    {
        private List<Semester> semesterList;
        private CompletedClasses completedClasses;
        private List<SemesterView> semesterViews;
        private YAMLLoader loader;

        public TentativeSchedule(List<Semester> semesters, CompletedClasses prevPage, YAMLLoader passedLoader)
        {
            InitializeComponent();
            this.loader = passedLoader;
            this.semesterList = semesters;
            completedClasses = prevPage;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            FlowLayoutPanel fallpanel = new FlowLayoutPanel();
            FlowLayoutPanel springpanel = new FlowLayoutPanel();
            fallpanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            springpanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;

            //var semesterList = new JsonLoader().loadScheduleList("Strategic_Advising.res.SampleSchedule.json");

            bool isFall = this.completedClasses.getIsFall();
            semesterViews = new List<SemesterView>();
            if(semesterList[0].isFall == false)
            {
                fallpanel.Controls.Add(new SemesterView());
            }
            foreach (Semester sem in semesterList)
            {
                SemesterView semView = new SemesterView(sem);
                
                semView.CellMouseClick += new DataGridViewCellMouseEventHandler(cellClick);

                if (sem.isFall)
                {
                    fallpanel.Controls.Add(semView);
                    semesterViews.Add(semView);
                }
                else
                {
                    springpanel.Controls.Add(semView);
                    semesterViews.Add(semView);
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
            CompletedClasses window = completedClasses;
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
            SemesterView dgv = (sender as SemesterView);
            if (e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewCell cell = dgv[e.ColumnIndex, e.RowIndex];
                if (!cell.Selected)
                {
                    cell.DataGridView.ClearSelection();
                    cell.DataGridView.CurrentCell = cell;
                    cell.Selected = true;
                }
                System.Windows.Forms.ContextMenuStrip menuStrip = new System.Windows.Forms.ContextMenuStrip();

                ToolStripMenuItem move = new ToolStripMenuItem("Move a class");
                //move.Click += new EventHandler(moveClass);
                menuStrip.Items.Add(move);
                ToolStripMenuItem add = new ToolStripMenuItem("Add a class");
                add.Click += (sender2, e2) => addClass(sender2, e2, dgv);
                menuStrip.Items.Add(add);
                ToolStripMenuItem remove = new ToolStripMenuItem("Remove a class");
                remove.Click += (sender2, e2) => removeClass(sender2, e2, dgv);
                menuStrip.Items.Add(remove);

                bool isSemFall = completedClasses.getIsFall();
                int fallCounter = 1;
                int springCounter = 1;
                foreach(Semester sem in semesterList)
                {
                    int i = sem.position;
                    string menuItemName = "";
                    if (isSemFall)
                    {
                        menuItemName += "Fall " + fallCounter;
                        fallCounter++;
                    }
                    else
                    {
                        menuItemName += "Spring " + springCounter;
                        springCounter++;
                    }
                    ToolStripMenuItem item = new ToolStripMenuItem(menuItemName);
                    item.Tag = sem.position;
                    item.Click += (sender2, e2) => moveClass(sender2, e2, dgv);
                    move.DropDownItems.Add(item);
                    isSemFall = !isSemFall;
                }

                
                //menuStrip.Items.Add("Move a class");
                //(menuStrip.Items[0] as ToolStripMenuItem).DropDownItems.Add()
                //menuStrip.Items.Add("Add a class");
                //menuStrip.Items[1].Click += new EventHandler(addClass);
                //menuStrip.Items.Add("Remove a class");
                //menuStrip.Items[2].Click += new EventHandler(removeClass);

                //menuStrip.ItemClicked += new ToolStripItemClickedEventHandler(menuClick);
                menuStrip.Show(dgv, new System.Drawing.Point(e.X + 42, e.Y));
            }
        }

        private void moveClass(object sender, EventArgs e, SemesterView dgv)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            int destinationPosition = int.Parse(clickedItem.Tag.ToString());
            DataGridViewRow row = dgv.CurrentCell.OwningRow;
            Course course = row.DataBoundItem as Course;
            semesterViews[destinationPosition].addCourse(course);
            removeCourse(course, dgv);
        }

        private void addClass(object sender, EventArgs e, SemesterView dgv)
        {
            List<Course> coursesToAdd = getClasses();
            int semPosition = dgv.getSemesterPosition();
            dgv.addCourses(coursesToAdd);
            semesterList[semPosition] = dgv.getSemester();
        }

        private void removeClass(object sender, EventArgs e, SemesterView dgv)
        {
            DataGridViewRow row = dgv.CurrentCell.OwningRow;
            Course course = (Course)row.DataBoundItem;
            removeCourse(course, dgv);
        }

        private void menuClick(object sender, ToolStripItemClickedEventArgs e)
        {
            //use the following to retrieve dgv values:
            var strip = sender as ContextMenuStrip;
            var dgv = strip.SourceControl as SemesterView;
            DataGridViewRow row = dgv.CurrentRow;
             
            if(e.ClickedItem.Text == "Move a class")
            {
                //dropdow list of other semesters to choose from? 
                System.Windows.MessageBox.Show(row.Cells[0].Value.ToString());

            }
            if (e.ClickedItem.Text == "Add a class")
            {
                //seperate pop up to choose classes from

                //display pop up
                List<Course> coursesToAdd = getClasses();

                //get selecvted class
                //foreach (Semester sem in semesterList)
                //{
                //    BindingList<Course> dgvList = dgv.DataSource as BindingList<Course>;
                //    if (sem.classes[0].courseNumber == dgvList[0].courseNumber)
                //    {
                //        sem.classes.AddRange(coursesToAdd);
                //        dgv.DataSource = sem.classes;
                //        break;
                //    }
                //}
                int semPosition = dgv.getSemesterPosition();
                dgv.addCourses(coursesToAdd);
                semesterList[semPosition] = dgv.getSemester();
                
                //we need to find a way to grab what semester were operating in


                //System.Windows.MessageBox.Show(row.Cells[0].Value.ToString());
            }
            if (e.ClickedItem.Text == "Remove a class")
            {
                //pop up confirming decision?
                Course course = (Course)row.DataBoundItem;
                removeCourse(course, dgv);            
            }
        }

        private void removeCourse(Course course, SemesterView dgv)
        {
            int semPosition = dgv.getSemesterPosition();
            dgv.removeCourse(course);
            semesterList[semPosition] = dgv.getSemester();
            //foreach (Semester sem in semesterList)
            //{
            //    //temp.Remove(temp.Find(x => x.courseNumber == course.courseNumber));
            //    List<Course> courseList = sem.classes;
            //    if (courseList.Remove(courseList.Find(x => x.courseNumber == course.courseNumber)))
            //    {
            //        dgv.DataSource = courseList;
            //        break;
            //    };

            //}
        }

        private List<Course> getClasses()
        {
            List<Course> coursesToAdd = new List<Course>();
            ClassSelectorWindow csWindow = new ClassSelectorWindow(this.loader);
            bool? dialogResult = csWindow.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    coursesToAdd = csWindow.selectedCourses();
                    break;
                case false:
                    break;
                default:
                    break;
            }
            return coursesToAdd;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
