using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class OfficialReport
    {
        public OfficialReport()
        {
            Picture = new HashSet<Picture>();
            SoundRecord = new HashSet<SoundRecord>();
        }

        public Guid officialReportId { get; set; }
        public Guid reporterId { get; set; }
        public Guid reportId { get; set; }
        public string observation { get; set; }
        public TimeSpan? time { get; set; }
        public string location { get; set; }

        public virtual Report report { get; set; }
        public virtual Officer reporter { get; set; }
        public virtual ICollection<Picture> Picture { get; set; }
        public virtual ICollection<SoundRecord> SoundRecord { get; set; }
    }
}