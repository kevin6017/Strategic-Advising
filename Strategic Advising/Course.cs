using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategic_Advising
{
    public class Course
    {
        public string courseNumber { get; set; }
        public string courseTitle { get; set; }
        public int creditHours { get; set; }
        public bool fall { get; set; }
        public bool spring { get; set; }


        public List<Course> prerequisites { get; set; }
        public int[] priority { get; internal set; }
    }
}
