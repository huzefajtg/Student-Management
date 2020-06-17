using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class CourseSubject
    {
        public CourseSubject()
        {
            Courses = new List<Courses>();
        }

        [Key]
        public int SubjectId { get; set; }
        [StringLength(30)]
        public string SubjectName { get; set; }

        [InverseProperty("Subject")]
        public ICollection<Courses> Courses { get; set; }
    }
}
