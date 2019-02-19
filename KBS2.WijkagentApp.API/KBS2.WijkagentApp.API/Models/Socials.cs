using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Socials
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid socialsId { get; set; }

        public Guid personId { get; set; }
        public string profile { get; set; }
    }
}