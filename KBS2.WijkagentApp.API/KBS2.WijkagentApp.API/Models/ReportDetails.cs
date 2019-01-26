using System;
using System.Collections.Generic;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class ReportDetails
    {
        public Guid reportId { get; set; }
        public Guid personId { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string statement { get; set; }
        public bool? isHeard { get; set; }

        public virtual Person person { get; set; }
        public virtual Report report { get; set; }
    }
}