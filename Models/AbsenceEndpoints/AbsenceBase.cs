using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    public partial class AbsenceBase
    {
        [Newtonsoft.Json.JsonProperty("number")]
        public string Number { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("beginDate")]
        public System.DateTime BeginDate { get; set; }

        [Newtonsoft.Json.JsonProperty("endDate")]
        public System.DateTime EndDate { get; set; }

        [Newtonsoft.Json.JsonProperty("status")]
        public string Status { get; set; }

        [Newtonsoft.Json.JsonProperty("statusIdentifier")]
        public string StatusIdentifier { get; set; }

        [Newtonsoft.Json.JsonProperty("workingDays")]
        public double WorkingDays { get; set; }

        /// <summary>
        /// Specifies whether the absence spans only the afternoon of the first day (or only the afternoon for one-day absences).
        /// </summary>
        [Newtonsoft.Json.JsonProperty("isBeginDateHalfDay")]
        public bool IsBeginDateHalfDay { get; set; }

        /// <summary>
        /// Specifies whether the absence spans only the forenoon of the last day (or only the forenoon for one-day absences).
        /// </summary>
        [Newtonsoft.Json.JsonProperty("isEndDateHalfDay")]
        public bool IsEndDateHalfDay { get; set; }
    }

    public partial class AbsenceBase
    {
        public override string ToString()
        {
            return string.Format("{0}: {1:dd.MM.yyyy} - {2:dd.MM.yyyy} = {3:f} ({4})", this.Name, this.BeginDate, this.EndDate, this.WorkingDays, this.Status);
        }
    }
}