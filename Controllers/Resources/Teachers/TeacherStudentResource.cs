using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class TeacherStudentResource
    {
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        //public int TableId { get; set; }

        public StudentResource Student { get; set; }
        public TeacherResource Teacher { get; set; }
    }
}
