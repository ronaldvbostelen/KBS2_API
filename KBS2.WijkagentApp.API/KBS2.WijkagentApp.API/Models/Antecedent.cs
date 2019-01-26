using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Antecedent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid antecedentId { get; set; }
        public Guid personId { get; set; }
        public string type { get; set; }
        public string verdict { get; set; }
        public string crime { get; set; }
        public string description { get; set; }

        public virtual Person person { get; set; }
    }
}