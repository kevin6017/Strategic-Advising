using System;
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
    class YAMLLoader
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

    }
}
