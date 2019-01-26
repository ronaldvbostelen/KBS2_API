using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class Picture
    {
        public Guid pictureId { get; set; }
        public Guid officialReportId { get; set; }
        public string URL { get; set; }

        public virtual OfficialReport officialReport { get; set; }
    }
}