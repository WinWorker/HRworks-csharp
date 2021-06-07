using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceActions
{
    public partial class Absence
    {
        [Newtonsoft.Json.JsonProperty("number")]
        public string Number { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("absenceTypeKey")]
        public string AbsenceTypeKey { get; set; }

        [Newtonsoft.Json.JsonProperty("beginDate")]
        public System.DateTime BeginDate { get; set; }

        [Newtonsoft.Json.JsonProperty("endDate")]
        public System.DateTime EndDate { get; set; }

        [Newtonsoft.Json.JsonProperty("status")]
        public string Status { get; set; }

        [Newtonsoft.Json.JsonProperty("workingDays")]
        public double WorkingDays { get; set; }

        [Newtonsoft.Json.JsonProperty("isForenoonHalfDay")]
        public bool IsForenoonHalfDay { get; set; }

        [Newtonsoft.Json.JsonProperty("isAfternoonHalfDay")]
        public bool IsAfternoonHalfDay { get; set; }
    }

    public partial class Absence
    {
        public override string ToString()
        {
            return string.Format("{0}: {1:dd.MM.yyyy} - {2:dd.MM.yyyy} = {3:f} ({4})", this.Name, this.BeginDate, this.EndDate, this.WorkingDays, this.Status);
        }
    }
}