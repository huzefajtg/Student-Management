using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class CourseStudentResource
    {
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int TableId { get; set; }
    }
}
