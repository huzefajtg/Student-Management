using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class NotiFicationTeachers
    {
        public int? NotificationFor { get; set; }
        [StringLength(100)]
        public string NotificationMessage { get; set; }
        [Column("viewed")]
        public bool? Viewed { get; set; }
        [Key]
        public int MessageId { get; set; }
        [StringLength(30)]
        public string MessageDate { get; set; }

        [ForeignKey("NotificationFor")]
        [InverseProperty("NotiFicationTeachers")]
        public Teachers NotificationForNavigation { get; set; }
    }
}
