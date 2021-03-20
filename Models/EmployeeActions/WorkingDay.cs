using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.EmployeeActions
{
    public partial class WorkingDay
    {
        [Newtonsoft.Json.JsonProperty("workingHours")]
        public long WorkingHours { get; set; }

        [Newtonsoft.Json.JsonProperty("day")]
        public string Day { get; set; }
    }

}
