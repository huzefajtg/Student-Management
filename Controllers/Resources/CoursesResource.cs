using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class CoursesResource
    {
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
        [StringLength(20)]
        public string Dob { get; set; }
    }
}
