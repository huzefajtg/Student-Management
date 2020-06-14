using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class Courses
    {
        public Courses()
        {
            CourseStudent = new HashSet<CourseStudent>();
            Teachers = new HashSet<Teachers>();
        }

        [Key]
        public int CourseId { get; set; }
        [StringLength(40)]
        public string CourseName { get; set; }
        public int? SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("Courses")]
        public CourseSubject Subject { get; set; }
        [InverseProperty("Course")]
        public ICollection<CourseStudent> CourseStudent { get; set; }
        [InverseProperty("Course")]
        public ICollection<Teachers> Teachers { get; set; }
    }
}
