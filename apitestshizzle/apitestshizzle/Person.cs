using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace apitestshizzle
{
    class Person
    {
        public string id { get; set; }

        [JsonProperty(PropertyName = "personId")]
        public Guid personId { get; set; }
        [JsonProperty(PropertyName = "socialSecurityNumber")]
        public int? socialSecurityNumber { get; set; }
        [JsonProperty(PropertyName = "firstName")]
        public string firstName { get; set; }
        [JsonProperty(PropertyName = "lastName")]
        public string lastName { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public string gender { get; set; }
        [JsonProperty(PropertyName = "birthDate")]
        public DateTime? birthDate { get; set; }
        [JsonProperty(PropertyName = "phoneNumber")]
        public string phoneNumber { get; set; }
        [JsonProperty(PropertyName = "emailAddress")]
        public string emailAddress { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
    }
}
