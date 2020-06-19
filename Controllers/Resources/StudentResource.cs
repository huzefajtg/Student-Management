using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class StudentResource
    {
        public int StudentId { get; set; }
        public bool? IsReg { get; set; }

        public PersonalResource PersonalInfo { get; set; }
    }
}
