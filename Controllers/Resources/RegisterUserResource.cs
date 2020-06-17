using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class PersonalResource
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public bool isReg { get; set; }

        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string Dob { get; set; }
        public string Type { get; set; }
    }

    public class RegisterUserResource
    {
        public PersonalResource PersonalInfo { get; set; }
        public UsernamePasswordResource UserInfo { get; set; }

    }
}
