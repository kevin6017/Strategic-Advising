﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategic_Advising
{
    class Course
    {
        public string courseNumber { get; set; }

        public string courseTitle { get; set; }

        public string creditHours { get; set; }

        public bool fall { get; set; }
        public bool spring { get; set; }
        public bool summer { get; set; }

        public string[] prerequisites { get; set; }

        public string courseDescription { get; set; }
    }
}
