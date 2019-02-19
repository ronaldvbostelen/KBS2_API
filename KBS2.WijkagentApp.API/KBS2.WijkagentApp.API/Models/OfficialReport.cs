using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class OfficialReport
    {
        [Key, ForeignKey(nameof(reportId))]
        public Guid reportId { get; set; }
        
        public Guid reporterId { get; set; }
        public string observation { get; set; }
        public DateTime? time { get; set; }
        public string location { get; set; }
    }
}