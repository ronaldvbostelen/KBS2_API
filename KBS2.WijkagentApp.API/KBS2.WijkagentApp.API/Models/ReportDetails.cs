using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class ReportDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid reportDetailsId { get; set; }

        public Guid reportId { get; set; }
        public Guid personId { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string statement { get; set; }
        public bool? isHeard { get; set; }
    }
}