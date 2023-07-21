using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    public partial class SickLeaveType : HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseType
    {
        [Newtonsoft.Json.JsonProperty("isSicknessOfChild")]
        public bool IsSicknessOfChild { get; set; }
    }

    public partial class SickLeaveType
    {
        public override bool IsSickLeave => true;

        public override string ToString()
        {
            return string.Format("{0} - {1} ({2})", this.Key, this.Name);
        }
    }
}