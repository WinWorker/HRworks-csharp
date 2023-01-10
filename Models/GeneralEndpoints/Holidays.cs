using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralEndpoints
{
    public partial class Holidays
    {
        [Newtonsoft.Json.JsonProperty("permamentEstablishmentHolidays")]
        public Dictionary<string, Holiday[]> PermamentEstablishmentHolidays { get; set; }

        [Newtonsoft.Json.JsonProperty("stateHolidays")]
        public Dictionary<string, Holiday[]> StateHolidays { get; set; }

        [Newtonsoft.Json.JsonProperty("generalHolidays")]
        public Holiday[] GeneralHolidays { get; set; }
    }
}