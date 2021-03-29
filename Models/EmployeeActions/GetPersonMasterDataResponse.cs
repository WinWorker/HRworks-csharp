using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.EmployeeActions
{
    public partial class GetPersonMasterDataResponse
    {
        [Newtonsoft.Json.JsonProperty("persons")]
        public IList<HRworksConnector.Models.EmployeeActions.Person> Persons { get; set; }
    }

    public partial class GetPersonMasterDataResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Persons", this.Persons.Count);
        }
    }
}