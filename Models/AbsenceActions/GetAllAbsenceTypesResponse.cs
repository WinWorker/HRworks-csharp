using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceActions
{
    public partial class GetAllAbsenceTypesResponse
    {
        [Newtonsoft.Json.JsonProperty("absenceTypes")]
        public IList<HRworksConnector.Models.AbsenceActions.AbsenceType> AbsenceTypes { get; set; }
    }

    public partial class GetAllAbsenceTypesResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Absence-Types", this.AbsenceTypes.Count);
        }

        public string[] GetActiveKrankAndUrlaubKeys()
        {
            System.Collections.Generic.List<string> activeKrankAndUrlaubKeys = new System.Collections.Generic.List<string>();

            foreach (HRworksConnector.Models.AbsenceActions.AbsenceType absenceType in this.AbsenceTypes)
            {
                if (!absenceType.IsActive)
                {
                    continue;
                }

                // Krank
                if (string.Compare(absenceType.Type, "sickLeave", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    activeKrankAndUrlaubKeys.Add(absenceType.Key);
                }

                // Urlaub
                if (string.Compare(absenceType.Key, "JA", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    activeKrankAndUrlaubKeys.Add(absenceType.Key);
                }
            }

            return activeKrankAndUrlaubKeys.ToArray();
        }
    }
}
