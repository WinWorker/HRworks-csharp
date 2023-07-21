using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.AbsenceEndpoints
{
    [Newtonsoft.Json.JsonConverter(typeof(GetSickLeavesConverter))]
    public class GetSickLeavesResponse
    {
        public IList<HRworksConnector.Models.AbsenceEndpoints.PersonSickLeaves> PersonSickLeaves { get; set; }

        public GetSickLeavesResponse()
        {
            this.PersonSickLeaves = new System.Collections.Generic.List<HRworksConnector.Models.AbsenceEndpoints.PersonSickLeaves>();
        }
    }

    public partial class PersonSickLeaves
    {
        public string Username { get; set; }

        public IList<HRworksConnector.Models.AbsenceEndpoints.AbsenceBase> SickLeaves { get; set; }

        public PersonSickLeaves()
        {
            this.SickLeaves = new System.Collections.Generic.List<HRworksConnector.Models.AbsenceEndpoints.AbsenceBase>();
        }
    }

    public partial class PersonSickLeaves
    {
        public override string ToString()
        {
            return string.Format("{0} - {1} Krankheitsbuchungen", this.Username, this.SickLeaves.Count);
        }
    }

    public class GetSickLeavesConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GetSickLeavesResponse);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Newtonsoft.Json.Linq.JObject getSickLeavesResponseAsJson = Newtonsoft.Json.Linq.JObject.Load(reader);

            GetSickLeavesResponse getSickLeavesResponse = new GetSickLeavesResponse();

            foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> personSickLeaves in getSickLeavesResponseAsJson)
            {
                string username = personSickLeaves.Key;
                Newtonsoft.Json.Linq.JArray sickLeavesList = personSickLeaves.Value as Newtonsoft.Json.Linq.JArray;

                HRworksConnector.Models.AbsenceEndpoints.PersonSickLeaves tmpPersonSickLeaves = new HRworksConnector.Models.AbsenceEndpoints.PersonSickLeaves();
                tmpPersonSickLeaves.Username = username;

                foreach (Newtonsoft.Json.Linq.JToken sickLeavesAsJson in sickLeavesList)
                {
                    Newtonsoft.Json.Linq.JObject sickLeaves = sickLeavesAsJson as Newtonsoft.Json.Linq.JObject;

                    Newtonsoft.Json.Linq.JToken tmpSickLeavesAsJson = sickLeaves.GetValue("sickLeaves", StringComparison.InvariantCultureIgnoreCase);

                    if (tmpSickLeavesAsJson != null)
                    {
                        Newtonsoft.Json.Linq.JArray sickLieavesAsJsonArray = tmpSickLeavesAsJson as Newtonsoft.Json.Linq.JArray;

                        foreach (Newtonsoft.Json.Linq.JObject sickLieaveAsJson in sickLieavesAsJsonArray)
                        {
                            HRworksConnector.Models.AbsenceEndpoints.AbsenceBase sickLeave = sickLieaveAsJson.ToObject<HRworksConnector.Models.AbsenceEndpoints.AbsenceBase>();
                            tmpPersonSickLeaves.SickLeaves.Add(sickLeave);
                        }

                        getSickLeavesResponse.PersonSickLeaves.Add(tmpPersonSickLeaves);
                    }
                }
            }

            return getSickLeavesResponse;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}