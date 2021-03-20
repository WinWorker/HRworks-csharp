using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralActions
{
    public partial class OrganizationUnit
    {
        [Newtonsoft.Json.JsonProperty("organizationUnitNumber")]
        public string OrganizationUnitNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("organizationUnitName")]
        public string OrganizationUnitName { get; set; }

        [Newtonsoft.Json.JsonProperty("parentOrganizationUnit")]
        public OrganizationUnit parentOrganizationUnit { get; set; }
    }

    public partial class OrganizationUnit
    {
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.OrganizationUnitName, this.OrganizationUnitNumber);
        }
    }
}
