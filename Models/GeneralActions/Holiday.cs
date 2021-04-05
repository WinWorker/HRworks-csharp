using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralActions
{
    public partial class Holiday
    {
        [Newtonsoft.Json.JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [Newtonsoft.Json.JsonProperty("isHalfDay")]
        public bool IsHalfDay { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
    }
}
