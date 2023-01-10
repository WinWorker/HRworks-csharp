using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.PersonEndpoints
{
    public partial class WorkSchedule
    {
        [Newtonsoft.Json.JsonProperty("workingDays")]
        public Dictionary<string, WorkingDay>[] WorkingDays { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
    }
}