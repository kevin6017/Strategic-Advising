using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


namespace Strategic_Advising
{
    class YAMLLoader
    {
        private List<Curriculum> curric = new List<Curriculum>();

        public YAMLLoader()
        {
            var ds = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            this.curric = ds.Deserialize<List<Curriculum>>(File.OpenText("..\\..\\res/MasterCourseList.eyaml"));
        }

        public List<Curriculum> getMasterList()
        {
            return this.curric;
        }

        public List<Course> getCurriculum(int index)
        { 
            return this.curric[index].ToList<Course>();
        }

    }
}
