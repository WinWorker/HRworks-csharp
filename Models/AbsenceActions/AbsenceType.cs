using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceActions
{
    public partial class AbsenceType
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [Newtonsoft.Json.JsonProperty("reducesHolidayEntitlement")]
        public bool ReducesHolidayEntitlement { get; set; }
    }

    public partial class AbsenceType
    {
        public override string ToString()
        {
            return string.Format("{0} - {1} ({2})", this.Key, this.Name, this.Type);
        }
    }
}