using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class Socials
    {
        public Guid socialsId { get; set; }
        public Guid personId { get; set; }
        public string profile { get; set; }

        public virtual Person person { get; set; }
    }
}