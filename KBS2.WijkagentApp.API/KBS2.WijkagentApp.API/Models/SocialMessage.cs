using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class SocialMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid socialMessageId { get; set; }

        public Guid socialsId { get; set; }
        public string message { get; set; }

        public virtual Socials social { get; set; }
    }
}