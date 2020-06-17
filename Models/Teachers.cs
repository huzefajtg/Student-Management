using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class Teachers
    {
        public Teachers()
        {
            TeacherStudent = new Collection<TeacherStudent>();
        }

        [Key]
        public int TeacherId { get; set; }
        public int? CourseId { get; set; }
        [Column("IsHOD")]
        public bool? IsHod { get; set; }
        public bool? IsReg { get; set; }

        //PERSONAL
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

        [InverseProperty("Teacher")]
        public ICollection<TeacherStudent> TeacherStudent { get; set; }
    }
}
