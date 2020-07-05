using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers.Resources
{
    public class StudentNotificationResource
    {
        public int NotificationId { get; set; }
        public int? StudentId { get; set; }
        public string NotiDate { get; set; }
        public string NotiMessage { get; set; }
        public int? OtherId { get; set; }
        public string OtherType { get; set; }
        public bool? Viwed { get; set; }

        public string OtherName { get; set; }
        public StudentResource Student { get; set; }
    }
}
