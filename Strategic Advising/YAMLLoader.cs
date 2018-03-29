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
        public void addCourseToCurriculum(int index, Course course)
        {
            this.curric[index].courses.Add(course);
        }

        public void serializeFile()
        {
            Serializer serial = new SerializerBuilder().Build();
            string yaml = serial.Serialize(this.curric);

        }

    }
}
