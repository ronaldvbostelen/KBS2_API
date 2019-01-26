using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Report
    {
        public Report()
        {
            OfficialReport = new HashSet<OfficialReport>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid reportId { get; set; }

        public Guid reporterId { get; set; }
        public Guid? processedBy { get; set; }
        public string type { get; set; }
        public TimeSpan? time { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public int? priority { get; set; }
        public string comment { get; set; }
        public decimal? longitude { get; set; }
        public decimal? latitude { get; set; }

        public virtual Person reporter { get; set; }
        public virtual ReportDetails ReportDetails { get; set; }
        public virtual ICollection<OfficialReport> OfficialReport { get; set; }
    }
}