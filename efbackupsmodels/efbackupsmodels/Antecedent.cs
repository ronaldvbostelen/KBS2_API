using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class Antecedent
    {
        public Guid antecedentId { get; set; }
        public Guid personId { get; set; }
        public string type { get; set; }
        public string verdict { get; set; }
        public string crime { get; set; }
        public string description { get; set; }

        public virtual Person person { get; set; }
    }
}