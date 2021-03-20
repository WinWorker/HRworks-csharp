using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralActions
{
    public partial class PersonsOfOrganizationUnitResponse
    {
        [Newtonsoft.Json.JsonProperty("persons")]
        public IList<HRworksConnector.Models.GeneralActions.PersonBaseData> Persons { get; set; }
    }

    public partial class PersonsOfOrganizationUnitResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Persons", this.Persons.Count);
        }
    }
}
