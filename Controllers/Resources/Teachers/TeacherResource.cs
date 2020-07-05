using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class TeacherResource
    {
        public PersonalResource PersonalInfo { get; set; }
        public KeyValuePairResource HOD { get; set; }
        public KeyValuePairResource SubjectInfo { get; set; }

        public string username { get; set; }
        public int TeacherId { get; set; }
        //public bool? IsHod { get; set; }
        //public bool? IsReg { get; set; }

        public CourseResource Course { get; set; }
        //public ICollection<TeacherStudent> TeacherStudent { get; set; }
    }
}
