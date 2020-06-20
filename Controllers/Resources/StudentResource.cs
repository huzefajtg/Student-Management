using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class StudentResource
    {
        public StudentResource()
        {
            teacherInfo = new List<TeacherResource>();
        }

        public int StudentId { get; set; }
        public PersonalResource PersonalInfo { get; set; }
        public List<TeacherResource> teacherInfo { get; set; }

    }
}
