using System;
using System.Collections.Generic;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Address
    {
        public Guid personId { get; set; }
        public string town { get; set; }
        public string zipcode { get; set; }
        public string street { get; set; }
        public string number { get; set; }
    }
}