using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public partial class LoginInfo
    {
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string UserPassword { get; set; }
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(2)]
        public string UserType { get; set; }
        [Key]
        public int Loginid { get; set; }
    }
}
