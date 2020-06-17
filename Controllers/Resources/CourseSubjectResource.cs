using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class CourseSubjectResource
    {
        public CourseSubjectResource()
        {
            Courses = new Collection<CourseResource>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public ICollection<CourseResource> Courses { get; set; }
    }
}
