using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralEndpoints
{
    public partial class PersonsOfOrganizationUnitResponse
    {
        [Newtonsoft.Json.JsonProperty("persons")]
        public IList<HRworksConnector.Models.PersonEndpoints.PersonBaseData> Persons { get; set; }
    }

    public partial class PersonsOfOrganizationUnitResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Persons", this.Persons.Count);
        }
    }
}