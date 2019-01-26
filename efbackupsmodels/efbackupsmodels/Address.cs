using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class Address
    {
        public Guid personId { get; set; }
        public string town { get; set; }
        public string zipcode { get; set; }
        public string street { get; set; }
        public string number { get; set; }

        public virtual Person person { get; set; }
    }
}