using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.PersonEndpoints
{
    public partial class Address
    {
        [Newtonsoft.Json.JsonProperty("street")]
        public string Street { get; set; }

        [Newtonsoft.Json.JsonProperty("additionalData")]
        public string AdditionalData { get; set; }

        [Newtonsoft.Json.JsonProperty("streetNumber")]
        public string StreetNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [Newtonsoft.Json.JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [Newtonsoft.Json.JsonProperty("city")]
        public string City { get; set; }
    }
}