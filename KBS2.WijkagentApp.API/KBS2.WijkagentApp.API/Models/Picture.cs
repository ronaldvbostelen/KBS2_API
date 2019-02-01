using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class Picture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid pictureId { get; set; }

        public Guid officialReportId { get; set; }
        public string URL { get; set; }
    }
}