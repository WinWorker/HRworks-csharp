using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    [Newtonsoft.Json.JsonConverter(typeof(GetAbsencesConverter))]
    public class GetAbsencesResponse
    {
        public IList<HRworksConnector.Models.AbsenceEndpoints.PersonAbsences> PersonAbsences { get; set; }

        public GetAbsencesResponse()
        {
            this.PersonAbsences = new System.Collections.Generic.List<HRworksConnector.Models.AbsenceEndpoints.PersonAbsences>();
        }
    }

    public partial class PersonAbsences
    {
        public string Username { get; set; }

        public IList<HRworksConnector.Models.AbsenceEndpoints.AbsenceBase> Absences { get; set; }

        public PersonAbsences()
        {
            this.Absences = new System.Collections.Generic.List<HRworksConnector.Models.AbsenceEndpoints.AbsenceBase>();
        }
    }

    public partial class PersonAbsences
    {
        public override string ToString()
        {
            return string.Format("{0} - {1} Abwesenheitsbuchungen", this.Username, this.Absences.Count);
        }
    }

    public class GetAbsencesConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GetAbsencesResponse);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Newtonsoft.Json.Linq.JObject getAbsencesResponseAsJson = Newtonsoft.Json.Linq.JObject.Load(reader);

            GetAbsencesResponse getAbsencesResponse = new GetAbsencesResponse();

            foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> personAbsences in getAbsencesResponseAsJson)
            {
                string username = personAbsences.Key;
                Newtonsoft.Json.Linq.JArray absencesList = personAbsences.Value as Newtonsoft.Json.Linq.JArray;

                HRworksConnector.Models.AbsenceEndpoints.PersonAbsences tmpPersonAbsences = new HRworksConnector.Models.AbsenceEndpoints.PersonAbsences();
                tmpPersonAbsences.Username = username;

                foreach (Newtonsoft.Json.Linq.JToken absencesAsJson in absencesList)
                {
                    Newtonsoft.Json.Linq.JObject absences = absencesAsJson as Newtonsoft.Json.Linq.JObject;

                    Newtonsoft.Json.Linq.JToken tmpAbsencesAsJson = absences.GetValue("absences", StringComparison.InvariantCultureIgnoreCase);

                    if (tmpAbsencesAsJson != null)
                    {
                        Newtonsoft.Json.Linq.JArray absencesAsJsonArray = tmpAbsencesAsJson as Newtonsoft.Json.Linq.JArray;

                        foreach (Newtonsoft.Json.Linq.JObject absenceAsJson in absencesAsJsonArray)
                        {
                            HRworksConnector.Models.AbsenceEndpoints.AbsenceBase absence = absenceAsJson.ToObject<HRworksConnector.Models.AbsenceEndpoints.AbsenceBase>();
                            tmpPersonAbsences.Absences.Add(absence);
                        }

                        getAbsencesResponse.PersonAbsences.Add(tmpPersonAbsences);
                    }
                }
            }

            return getAbsencesResponse;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}