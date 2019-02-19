using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Person
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid personId { get; set; }

        public int? socialSecurityNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public DateTime? birthDate { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string description { get; set; }
    }
}