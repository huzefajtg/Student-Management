using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class NotificationTeachersResource
    {
        public int NotificationFor { get; set; }
        public string NotificationMessage { get; set; }
        public bool Viewed { get; set; }
        public string MessageDate { get; set; }


        public TeacherResource NotificationForNavigation { get; set; }
    }
}
