using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralEndpoints
{
    public partial class OrganizationUnit
    {
        [Newtonsoft.Json.JsonProperty("number")]
        public string Number { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [Newtonsoft.Json.JsonProperty("sapCodeType")]
        public string SapCodeType { get; set; }

        [Newtonsoft.Json.JsonProperty("sapUnitCode")]
        public string SapUnitCode { get; set; }

        [Newtonsoft.Json.JsonProperty("parentOrganizationUnit")]
        public OrganizationUnit parentOrganizationUnit { get; set; }
    }

    public partial class OrganizationUnit
    {
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.Number);
        }
    }
}