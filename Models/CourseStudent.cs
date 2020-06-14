using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class CourseStudent
    {
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        [Key]
        [Column("TableID")]
        public int TableId { get; set; }

        [ForeignKey("CourseId")]
        [InverseProperty("CourseStudent")]
        public Courses Course { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("CourseStudent")]
        public Students Student { get; set; }
    }
}
