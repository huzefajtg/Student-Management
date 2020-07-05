using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class LoginInfoResource : UsernamePasswordResource
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public int Loginid { get; set; }
    }
}
