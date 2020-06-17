using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class TrackerResource
    {
        public int ByID { get; set; }
        public string ByType { get; set; }

        public int OnID { get; set; }
        public string OnType { get; set; }

        public string Operation { get; set; }
        public string Description { get; set; }
        public string DatePerformed { get; set; }

    }
}
