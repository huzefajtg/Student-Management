using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class Tracker
    {
        public int ById { get; set; }
        [StringLength(2)]
        public string ByType { get; set; }
        public int OnId { get; set; }
        [StringLength(2)]
        public string OnType { get; set; }
        [StringLength(20)]
        public string Operation { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(20)]
        public string DatePerformed { get; set; }
        [Key]
        public int TrackId { get; set; }
    }
}
