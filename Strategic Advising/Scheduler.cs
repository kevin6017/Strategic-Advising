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
    class Scheduler
    {
        
        private  HashSet<Course> prSet;
        private  List<Course> remainingCourseList;
        private  List<Semester> semesterList = new List<Semester>();
        

        public Scheduler(List<Course> coursesAlreadyTaken, int numberOfSemesters, bool nextSemesterIsFall, int coreIndex, int majorIndex)
        {
            var ds = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            List<Curriculum> curric = new YAMLLoader().getMasterList();
            remainingCourseList = buildRemainingCourseList(coreIndex, majorIndex, curric, coursesAlreadyTaken);
            prSet = new HashSet<Course>();
            initializePriorities();
            buildPrereqList();
            prioritizeCourses();
            assignClassDependencyNum();
            buildSemesterList(numberOfSemesters, nextSemesterIsFall);
            printSemesters();
        }

        public List<Semester> getSemesterList()
        {
            return semesterList;
        }

        private List<Course> buildRemainingCourseList(int coreIndex, int majorIndex, List<Curriculum> curric, List<Course> coursesAlreadyTaken)
        {
            List<Course> temp = new List<Course>();
            temp.AddRange(curric[coreIndex].ToList<Course>());
            temp.AddRange(curric[majorIndex].ToList<Course>());
            foreach (Course course in coursesAlreadyTaken)
            {
                temp.Remove(temp.Find(x => x.courseNumber == course.courseNumber));
            }
            return temp;

        }

        private void initializePriorities()
        {
            foreach (Course course in remainingCourseList)
            {
                int standardPriority = 0;
                int semesterOfferingPriority = (course.fall && course.spring ? 0 : 1);
                int courseDependenciesPriority = 1;
                course.priority = new int[3] { standardPriority, semesterOfferingPriority, courseDependenciesPriority };
            }
        }

        private void buildPrereqList()
        {
            foreach (Course course in remainingCourseList)
            {
                if (course.prerequisites != null)
                {
                    foreach (Course prereq in course.prerequisites)
                    {
                        prSet.Add(prereq);
                    }
                }
            }
        }

        private void prioritizeCourses()
        {
            foreach (Course course in remainingCourseList)
            {
                if (!prSet.Contains(course))
                {
                    assignPriorityToPrereqs(course);
                }
            }
        }

        private void assignPriorityToPrereqs(Course course)
        {
            if (course.prerequisites != null)
            {
                foreach (Course prereq in course.prerequisites)
                {
                    if(prereq.priority == null)
                    {
                        continue;   
                    }
                    if (prereq.priority[0] <= course.priority[0])
                    {
                        prereq.priority[0]++;
                    }

                    assignPriorityToPrereqs(prereq);
                }
            }
        }

        private void assignClassDependencyNum()
        {
            remainingCourseList = remainingCourseList.OrderBy(x => x.priority[0]).ToList();
            foreach (Course course in remainingCourseList)
            {
                if (course.prerequisites != null)
                {
                    foreach (Course prereq in course.prerequisites)
                    {
                        if(prereq.priority == null)
                        {
                            continue;
                        }
                        prereq.priority[2] += course.priority[2];
                    }
                }
            }
        }

        private void buildSemesterList(int numSemesters, bool isFall)
        {
            //Assign according to user input
            int semestersToGo = numSemesters;
            bool isFallTracker = isFall;

            int totalCreditsToGo = findTotalCredits();
            int targetHours = totalCreditsToGo / semestersToGo;
            List<Course> classList = new List<Course>();
            sortCoursesForScheduling();
            int semesterPosition = 0;
            while (remainingCourseList.Count > 0)
            {
                Semester currentSemester = new Semester();
                currentSemester.isFall = isFallTracker;
                currentSemester.totalCreditHours = 0;
                int currentClassIndex = 0;
                currentSemester.classes = new List<Course>();

                while (currentSemester.totalCreditHours < targetHours && currentClassIndex < remainingCourseList.Count)
                {
                    if (clearsChecks(currentSemester, remainingCourseList[currentClassIndex]))
                    {
                        currentSemester.classes.Add(remainingCourseList[currentClassIndex]);
                        currentSemester.totalCreditHours += remainingCourseList[currentClassIndex].creditHours;
                        remainingCourseList.RemoveAt(currentClassIndex);
                    }
                    else
                    {
                        currentClassIndex += 1;
                        if (currentClassIndex > remainingCourseList.Count)
                        {
                            break;
                        }
                    }
                }
                semesterList.Add(currentSemester);
                isFallTracker = !isFallTracker;
                currentSemester.position = semesterPosition;
                semesterPosition++;
            }
        }

        private void sortCoursesForScheduling()
        {
            remainingCourseList.Sort(new SortClasses());
        }

        private int findTotalCredits()
        {
            int creditCounter = 0;
            foreach (Course course in remainingCourseList)
            {
                creditCounter += course.creditHours;
            }
            return creditCounter;
        }

        private bool clearsChecks(Semester currentSemester, Course currentCourse)
        {
            if (currentCourse.creditHours + currentSemester.totalCreditHours > 18)
            {
                return false;
            }
            if (!(currentCourse.prerequisites == null || currentSemester.classes.Count == 0))
            {
                foreach (Course prereq in currentCourse.prerequisites)
                {
                    if (currentSemester.classes.Contains(prereq) || remainingCourseList.Contains(prereq))
                    {
                        return false;
                    }
                }
            }
            if (currentSemester.isFall)
            {
                return currentCourse.fall;
            }
            else //spring semester
            {
                return currentCourse.spring;
            }
        }

        private void printClassInfo(Course currentCourse)
        {
            //+ "  Course Title: " + currentCourse.courseTitle 
            string output = "Course Number: " + currentCourse.courseNumber + "  Priority: " + currentCourse.priority[0] + " Single Semester Offering: " + currentCourse.priority[1] + "# of Courses w/ Dependency: " + currentCourse.priority[2] + " Credit Hours: " + currentCourse.creditHours;
            Console.WriteLine(output);
        }

        private void printSemesters()
        {
            int counter = 0;
            foreach(Semester sem in semesterList)
            {
                Console.WriteLine("Semester: " + counter + ",  Total Credits: " + sem.totalCreditHours);
                foreach(Course course in sem.classes)
                {
                    printClassInfo(course);
                }
                counter++; 
            }
        }

        public class SortClasses : Comparer<Course>
        {
            public override int Compare(Course x, Course y)
            {
                if (x.priority[0].CompareTo(y.priority[0]) != 0)
                {
                    return (x.priority[0].CompareTo(y.priority[0])) * -1;
                }
                else if (x.priority[1].CompareTo(y.priority[1]) != 0)
                {
                    return (x.priority[1].CompareTo(y.priority[1])) * -1;
                }
                else if (x.priority[2].CompareTo(y.priority[2]) != 0)
                {
                    return (x.priority[2].CompareTo(y.priority[2])) * -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
