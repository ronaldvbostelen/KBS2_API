using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class PushMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid pushMessageId { get; set; }
        public Guid officerId { get; set; }
        public string message { get; set; }
        public DateTime? time { get; set; }
        public string location { get; set; }
        public decimal? longitude { get; set; }
        public decimal? latitude { get; set; }
        public string status { get; set; }
    }
}