using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Emergency
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid emergencyId { get; set; }

        public Guid officerId { get; set; }
        public string location { get; set; }
        public DateTime? time { get; set; }
        public string description { get; set; }
        public decimal? longitude { get; set; }
        public decimal? latitude { get; set; }
        public string status { get; set; }
    }
}