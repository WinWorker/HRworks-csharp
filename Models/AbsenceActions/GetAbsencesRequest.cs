using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceActions
{
    public partial class GetAbsencesRequest
    {
        [Newtonsoft.Json.JsonProperty("beginDate")]
        [Newtonsoft.Json.JsonConverter(typeof(HRworksConnector.Utils.Converter.DefaultDateTimeConverter))]
        public System.DateTime BeginDate { get; set; }

        [Newtonsoft.Json.JsonProperty("endDate")]
        [Newtonsoft.Json.JsonConverter(typeof(HRworksConnector.Utils.Converter.DefaultDateTimeConverter))]
        public System.DateTime EndDate { get; set; }

        [Newtonsoft.Json.JsonProperty("persons")]
        public IList<string> PersonnelNumbers { get; set; }

        [Newtonsoft.Json.JsonProperty("usePersonnelNumbers")]
        private bool UsePersonnelNumbers { get; } = true;

        public GetAbsencesRequest()
        {
            this.PersonnelNumbers = new System.Collections.Generic.List<string>();
        }
    }
}
