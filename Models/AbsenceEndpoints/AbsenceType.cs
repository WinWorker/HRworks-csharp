using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    public partial class AbsenceType : HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseType
    {
        [Newtonsoft.Json.JsonProperty("reducesHolidayEntitlement")]
        public bool ReducesHolidayEntitlement { get; set; }

        [Newtonsoft.Json.JsonProperty("reducesTimeAccount")]
        public bool ReducesTimeAccount { get; set; }

        [Newtonsoft.Json.JsonProperty("ignoreVacationBlackout")]
        public bool IgnoreVacationBlackout { get; set; }

        [Newtonsoft.Json.JsonProperty("maxVacationDaysPerYear")]
        public bool MaxVacationDaysPerYear { get; set; }

        [Newtonsoft.Json.JsonProperty("isSubstitutionMandatory")]
        public bool IsSubstitutionMandatory { get; set; }

        [Newtonsoft.Json.JsonProperty("reducesTargetWorkingHours")]
        public bool ReducesTargetWorkingHours { get; set; }
    }

    public partial class AbsenceType
    {
        public override bool IsSickLeave => false;

        public override string ToString()
        {
            return string.Format("{0} - {1} ({2})", this.Key, this.Name);
        }
    }
}