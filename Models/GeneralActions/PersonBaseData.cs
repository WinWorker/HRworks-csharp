using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralActions
{
    public partial class PersonBaseData
    {
        [Newtonsoft.Json.JsonProperty("personId")]
        public string PersonId { get; set; }

        [Newtonsoft.Json.JsonProperty("firstName")]
        public string FirstName { get; set; }

        [Newtonsoft.Json.JsonProperty("lastName")]
        public string LastName { get; set; }

        [Newtonsoft.Json.JsonProperty("personnelNumber")]
        public string PersonnelNumber { get; set; }
    }

    public partial class PersonBaseData
    {
        public override string ToString()
        {
            return string.Format("{0} - {1}, {2} ({3})", this.PersonId, this.LastName, this.FirstName, this.PersonnelNumber);
        }
    }
}