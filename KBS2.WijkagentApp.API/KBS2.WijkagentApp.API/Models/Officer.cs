using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Officer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid officerId { get; set; }

        public Guid? personId { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string salt { get; set; }
    }
}