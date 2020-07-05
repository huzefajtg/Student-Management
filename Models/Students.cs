using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class Students
    {
        public Students()
        {
            CourseStudent = new HashSet<CourseStudent>();
            StudentNotification = new HashSet<StudentNotification>();
            TeacherStudent = new HashSet<TeacherStudent>();
        }

        [Key]
        public int StudentId { get; set; }
        public bool? IsReg { get; set; }
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string SecondName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        [StringLength(3)]
        public string Gender { get; set; }
        [StringLength(20)]
        public string EmailId { get; set; }
        [StringLength(20)]
        public string ContactNumber { get; set; }
        [StringLength(40)]
        public string ContactAddress { get; set; }
        [Column("DOB")]
        [StringLength(20)]
        public string Dob { get; set; }

        [InverseProperty("Student")]
        public ICollection<CourseStudent> CourseStudent { get; set; }
        [InverseProperty("Student")]
        public ICollection<StudentNotification> StudentNotification { get; set; }
        [InverseProperty("Student")]
        public ICollection<TeacherStudent> TeacherStudent { get; set; }
    }
}
