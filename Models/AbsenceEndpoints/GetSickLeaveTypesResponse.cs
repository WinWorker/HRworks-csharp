using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    public partial class GetSickLeaveTypesResponse
    {
        [Newtonsoft.Json.JsonProperty("sickLeaveTypes")]
        public IList<HRworksConnector.Models.AbsenceEndpoints.SickLeaveType> SickLeaveTypes { get; set; }
    }

    public partial class GetSickLeaveTypesResponse
    {
        public override string ToString()
        {
            return string.Format("{0} SickLeave-Types", this.SickLeaveTypes.Count);
        }
    }
}