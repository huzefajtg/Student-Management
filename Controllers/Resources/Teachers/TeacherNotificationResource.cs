using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class TeacherNotificationResource
    {
        public int NotificationId { get; set; }
        public int? TeacherId { get; set; }
        public string NotiDate { get; set; }
        public string NotiMessage { get; set; }
        public int? OtherId { get; set; }
        public string OtherType { get; set; }
        public bool? Viwed { get; set; }

        public TeacherResource Teacher { get; set; }
        public string OtherName { get; set; }

    }
}
