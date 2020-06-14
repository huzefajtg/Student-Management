using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    [Table("registration")]
    public partial class Registration
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(20)]
        public string Name { get; set; }
        [Column("addres")]
        [StringLength(20)]
        public string Addres { get; set; }
    }
}
