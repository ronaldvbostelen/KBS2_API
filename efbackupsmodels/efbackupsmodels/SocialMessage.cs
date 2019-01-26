using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class SocialMessage
    {
        public Guid socialMessageId { get; set; }
        public Guid socialsId { get; set; }
        public string message { get; set; }
    }
}