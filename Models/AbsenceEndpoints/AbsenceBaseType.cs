using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    public abstract class AbsenceBaseType
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("key")]
        public string Key { get; set; }

        [Newtonsoft.Json.JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [Newtonsoft.Json.JsonProperty("useInMonthPayroll")]
        public bool UseInMonthPayroll { get; set; }

        [Newtonsoft.Json.JsonProperty("isAdminOnly")]
        public bool IsAdminOnly { get; set; }

        [Newtonsoft.Json.JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        [Newtonsoft.Json.JsonProperty("color")]
        public string Color { get; set; }

        public abstract bool IsSickLeave { get; }
    }

    public class AbsenceBaseTypeCollection : System.Collections.Generic.List<HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseType>
    {
        public string[] GetActiveKrankAndUrlaubKeys()
        {
            System.Collections.Generic.List<string> activeKrankAndUrlaubKeys = new System.Collections.Generic.List<string>();

            foreach (HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseType absenceBaseType in this)
            {
                if (!absenceBaseType.IsActive)
                {
                    continue;
                }

                // Krank
                // if (string.Compare(absenceType.Type, "sickLeave", StringComparison.OrdinalIgnoreCase) == 0)
                if (absenceBaseType.IsSickLeave)
                {
                    activeKrankAndUrlaubKeys.Add(absenceBaseType.Key);
                }

                // Urlaub
                if (string.Compare(absenceBaseType.Key, "JA", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    activeKrankAndUrlaubKeys.Add(absenceBaseType.Key);
                }

                if (string.Compare(absenceBaseType.Key, "UU", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    activeKrankAndUrlaubKeys.Add(absenceBaseType.Key);
                }
            }

            return activeKrankAndUrlaubKeys.ToArray();
        }
    }
}