using System;
using System.Collections.Generic;

namespace efbackupsmodels
{
    public partial class Person
    {
        public Person()
        {
            Antecedent = new HashSet<Antecedent>();
            Officer = new HashSet<Officer>();
            Report = new HashSet<Report>();
            ReportDetails = new HashSet<ReportDetails>();
            Socials = new HashSet<Socials>();
        }

        public Guid personId { get; set; }
        public int? socialSecurityNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public DateTime? birthDate { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string description { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Antecedent> Antecedent { get; set; }
        public virtual ICollection<Officer> Officer { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<ReportDetails> ReportDetails { get; set; }
        public virtual ICollection<Socials> Socials { get; set; }
    }
}