using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.EmployeeActions
{
    public partial class GetPersonMasterDataRequest
    {
        [Newtonsoft.Json.JsonProperty("persons")]
        public IList<string> PersonnelNumbers { get; set; }

        [Newtonsoft.Json.JsonProperty("usePersonnelNumbers")]
        private bool UsePersonnelNumbers { get; } = true;

        public GetPersonMasterDataRequest()
        {
            this.PersonnelNumbers = new System.Collections.Generic.List<string>();
        }
    }
}
