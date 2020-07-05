using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class TeacherNotification
    {
        [Key]
        public int NotificationId { get; set; }
        public int? TeacherId { get; set; }
        [StringLength(30)]
        public string NotiDate { get; set; }
        [StringLength(100)]
        public string NotiMessage { get; set; }
        public int? OtherId { get; set; }
        [StringLength(3)]
        public string OtherType { get; set; }
        [Column("viwed")]
        public bool? Viwed { get; set; }

        [ForeignKey("TeacherId")]
        [InverseProperty("TeacherNotification")]
        public Teachers Teacher { get; set; }
    }
}
