using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    public partial class GetAllAbsenceTypesResponse
    {
        [Newtonsoft.Json.JsonProperty("absenceTypes")]
        public IList<HRworksConnector.Models.AbsenceEndpoints.AbsenceType> AbsenceTypes { get; set; }
    }

    public partial class GetAllAbsenceTypesResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Absence-Types", this.AbsenceTypes.Count);
        }
    }
}