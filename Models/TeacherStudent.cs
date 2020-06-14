using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class TeacherStudent
    {
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        [Key]
        [Column("TableID")]
        public int TableId { get; set; }

        [ForeignKey("StudentId")]
        [InverseProperty("TeacherStudent")]
        public Students Student { get; set; }
        [ForeignKey("TeacherId")]
        [InverseProperty("TeacherStudent")]
        public Teachers Teacher { get; set; }
    }
}
