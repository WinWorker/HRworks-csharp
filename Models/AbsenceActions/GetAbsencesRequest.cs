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

        /// <summary>
        /// This parameter can be used to apply a filter to the result by specifying a
        /// list of absence type identifiers/keys.If set, only absences matching one of
        /// those keys will be returned.
        /// Note: The absence type keys can be retrieved via the
        /// GetAllAbsenceTypes action.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("types")]
        public IList<string> Types { get; set; }

        [Newtonsoft.Json.JsonProperty("usePersonnelNumbers")]
        private bool UsePersonnelNumbers { get; } = true;

        public GetAbsencesRequest()
        {
            this.PersonnelNumbers = new System.Collections.Generic.List<string>();
            this.Types = new System.Collections.Generic.List<string>();
        }
    }
}