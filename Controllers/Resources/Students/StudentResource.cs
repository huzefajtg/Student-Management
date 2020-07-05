using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class StudentResource
    {
        public StudentResource()
        {
            teacherInfo = new Collection<TeacherResource>();
        }

        public int StudentId { get; set; }
        public PersonalResource PersonalInfo { get; set; }
        public ICollection<TeacherResource> teacherInfo { get; set; }

    }
}
