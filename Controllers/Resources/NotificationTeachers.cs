using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class NotificationTeachersResource
    {
        /// <summary>
        /// /DO NOT USE
        /// </summary>
        public int NotificationId { get; set; }
        public int NotificationFor { get; set; }
        public string NotificationMessage { get; set; }
        public bool Viewed { get; set; }
        public string MessageDate { get; set; }


        public TeacherResource NotificationForNavigation { get; set; }
    }
}
