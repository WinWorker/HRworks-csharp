using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.EmployeeActions
{
    public partial class Cost
    {
        [Newtonsoft.Json.JsonProperty("number")]
        public string Number { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
    }
}