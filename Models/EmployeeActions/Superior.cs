using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.EmployeeActions
{
    public partial class Superior
    {
        [Newtonsoft.Json.JsonProperty("lastName")]
        public string LastName { get; set; }

        [Newtonsoft.Json.JsonProperty("personId")]
        public string PersonId { get; set; }

        [Newtonsoft.Json.JsonProperty("firstName")]
        public string FirstName { get; set; }

        [Newtonsoft.Json.JsonProperty("personnelNumber")]
        public string PersonnelNumber { get; set; }
    }
}