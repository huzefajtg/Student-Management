using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class LoginInfoResource
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int Id { get; set; }
        public string UserType { get; set; }
        public int Loginid { get; set; }
    }
}
