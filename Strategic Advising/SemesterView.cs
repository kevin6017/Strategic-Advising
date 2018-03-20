using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strategic_Advising
{
    class SemesterView : DataGridView
    {
        private Semester semester;
        private BindingList<Course> courseList;

        public SemesterView(Semester sem)
        {
            this.semester = sem;
            this.Width = 600;
            this.courseList = new BindingList<Course>(semester.classes.ToList<Course>());
            this.DataSource = courseList;
            this.AllowUserToAddRows = false;
        }

        public int getSemesterPosition()
        {
            return this.semester.position;
        }

        public void removeCourse(Course course)
        {
            courseList.Remove(course);
        }

        public Semester getSemester()
        {
            semester.classes = courseList.ToList<Course>();
            return this.semester;
        }

        public void addCourses(List<Course> coursesToAdd)
        {
            foreach(Course course in coursesToAdd)
            {
                courseList.Add(course);
            }
        }

        public void addCourse(Course course)
        {
            courseList.Add(course);
        }

    }
}
