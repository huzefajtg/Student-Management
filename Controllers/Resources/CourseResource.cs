using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class CourseResource
    {
        public CourseResource()
        {
            CourseStudent = new Collection<CourseStudentResource>();
            Teachers = new Collection<TeacherResource>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? SubjectId { get; set; }


        public ICollection<CourseStudentResource> CourseStudent { get; set; }
        public ICollection<TeacherResource> Teachers { get; set; }
    }
}


