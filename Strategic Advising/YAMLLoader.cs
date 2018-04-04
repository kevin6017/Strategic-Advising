﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Reflection;


namespace Strategic_Advising
{
    public class YAMLLoader
    {
        private List<Curriculum> curric = new List<Curriculum>();

        public YAMLLoader()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var temp = assembly.GetManifestResourceStream("Strategic_Advising.res.MasterCourseList.eyaml");
            StreamReader rdr = new StreamReader(temp);
            string yaml = rdr.ReadToEnd();
            var ds = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            this.curric = ds.Deserialize<List<Curriculum>>(yaml);
        }

        public List<Curriculum> getMasterList()
        {
            return this.curric;
        }

        public List<Course> getCurriculum(int index)
        {
            return this.curric[index].courses;
        }

        //may not need this stuff
        public void addCourseToCurriculum(int index, Course course, List<Course> prereqs)
        {
            if (prereqs != null)
            {
                foreach (Course p in prereqs)
                {
                    course.prerequisites.Add(getCourseFromMasterList(p));
                }
            }
            this.curric[0].courses.Add(course);
            this.curric[index].courses.Add(this.curric[0].courses.Find(x => x.courseNumber == course.courseNumber));
            
          
        }

        private Course getCourseFromMasterList(Course course)
        {
            return this.curric[0].courses.Find(x => x.courseNumber == course.courseNumber);
        }

        public void serializeFile()
        {
            Serializer serial = new SerializerBuilder().Build();
            string yaml = serial.Serialize(this.curric);
            //File.WriteAllText("Strategic_Advising.res.MasterCourseList.eyaml", yaml);
        }

        public void setMasterList(List<Curriculum> curricList)
        {
            this.curric = curricList;
        }

        public void changeCourseInfo(string oldCourseNum, Course newCourse)
        {
            int temp = this.curric[0].courses.FindIndex(x => x.courseNumber == oldCourseNum);

            this.curric[0].courses[temp].courseNumber = newCourse.courseNumber;
            this.curric[0].courses[temp].courseTitle = newCourse.courseTitle;
            this.curric[0].courses[temp].creditHours = newCourse.creditHours;
            this.curric[0].courses[temp].spring = newCourse.spring;
            this.curric[0].courses[temp].fall= newCourse.fall;
            this.curric[0].courses[temp].prerequisites = null;
            if (newCourse.prerequisites!= null)
            {
                foreach (Course p in newCourse.prerequisites)
                {
                    this.curric[0].courses[temp].prerequisites.Add(getCourseFromMasterList(p));
                }
            }
        }

        public void removeCourse(Course course)
        {
            int temp = this.curric[0].courses.FindIndex(x => x.courseNumber == course.courseNumber);
            foreach(Course c in this.curric[0].courses)
            {
                if (c.prerequisites != null)
                {
                    int numRemoved = c.prerequisites.RemoveAll(x => x.courseNumber == course.courseNumber);
                    if (numRemoved != 0)
                    {
                        if (course.prerequisites != null)
                        {
                            c.prerequisites.AddRange(course.prerequisites);
                        }
                        //foreach(Course prereq in course.prerequisites)
                        //{
                        //    c.prerequisites.Add(getCourseFromMasterList(prereq));
                        //}
                    }
                }
            }
            for(int i =0; i< this.curric.Count; i++)
            {
                this.curric[i].courses.RemoveAll(x => x.courseNumber == course.courseNumber);
            }
        }

    }
}
