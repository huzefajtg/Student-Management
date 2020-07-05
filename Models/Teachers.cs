using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class Teachers
    {
        public Teachers()
        {
            NotiFicationTeachers = new HashSet<NotiFicationTeachers>();
            TeacherNotification = new HashSet<TeacherNotification>();
            TeacherStudent = new HashSet<TeacherStudent>();
        }

        [Key]
        public int TeacherId { get; set; }
        public int? CourseId { get; set; }
        [Column("IsHOD")]
        public bool? IsHod { get; set; }
        public bool? IsReg { get; set; }
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string SecondName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        [StringLength(3)]
        public string Gender { get; set; }
        [StringLength(40)]
        public string EmailId { get; set; }
        [StringLength(20)]
        public string ContactNumber { get; set; }
        [StringLength(40)]
        public string ContactAddress { get; set; }
        [Column("DOB")]
        [StringLength(20)]
        public string Dob { get; set; }

        [ForeignKey("CourseId")]
        [InverseProperty("Teachers")]
        public Courses Course { get; set; }
        [InverseProperty("NotificationForNavigation")]
        public ICollection<NotiFicationTeachers> NotiFicationTeachers { get; set; }
        [InverseProperty("Teacher")]
        public ICollection<TeacherNotification> TeacherNotification { get; set; }
        [InverseProperty("Teacher")]
        public ICollection<TeacherStudent> TeacherStudent { get; set; }
    }
}
