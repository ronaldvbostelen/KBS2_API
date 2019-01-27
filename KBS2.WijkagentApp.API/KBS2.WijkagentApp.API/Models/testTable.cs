using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class testTable
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public string something { get; set; }
        public string alsosomething { get; set; }
    }
}