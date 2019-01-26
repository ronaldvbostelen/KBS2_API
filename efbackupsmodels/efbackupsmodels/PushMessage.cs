using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class PushMessage
    {
        public Guid pushMessageId { get; set; }
        public Guid officerId { get; set; }
        public string message { get; set; }
        public TimeSpan? time { get; set; }
        public string location { get; set; }
        public decimal? longitude { get; set; }
        public decimal? latitude { get; set; }

        public virtual Officer officer { get; set; }
    }
}