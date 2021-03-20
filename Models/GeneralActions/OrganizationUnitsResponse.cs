using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralActions
{
    public partial class OrganizationUnitsResponse
    {
        [Newtonsoft.Json.JsonProperty("organizationUnits")]
        public IList<HRworksConnector.Models.GeneralActions.OrganizationUnit> OrganizationUnits { get; set; }
    }

    public partial class OrganizationUnitsResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Organization Units", this.OrganizationUnits.Count);
        }
    }
}
