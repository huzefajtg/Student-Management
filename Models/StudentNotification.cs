using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class StudentNotification
    {
        [Key]
        public int NotificationId { get; set; }
        public int? StudentId { get; set; }
        [StringLength(30)]
        public string NotiDate { get; set; }
        [StringLength(100)]
        public string NotiMessage { get; set; }
        public int? OtherId { get; set; }
        [StringLength(3)]
        public string OtherType { get; set; }
        [Column("viwed")]
        public bool? Viwed { get; set; }

        [ForeignKey("StudentId")]
        [InverseProperty("StudentNotification")]
        public Students Student { get; set; }
    }
}
