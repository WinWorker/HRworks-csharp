using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.PersonEndpoints
{
    [Newtonsoft.Json.JsonConverter(typeof(GetAvailableWorkingHoursConverter))]
    public partial class GetAvailableWorkingHoursResponse
    {
        public IList<HRworksConnector.Models.PersonEndpoints.PersonWorkingHours> PersonsWorkingHours { get; set; }

        public GetAvailableWorkingHoursResponse()
        {
            this.PersonsWorkingHours = new System.Collections.Generic.List<HRworksConnector.Models.PersonEndpoints.PersonWorkingHours>();
        }
    }

    public partial class GetAvailableWorkingHoursResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Working Hours", this.PersonsWorkingHours.Sum(op => op.WorkingHours.Count));
        }
    }

    public class PersonWorkingHours
    {
        public string OrganizationUnitNumber { get; set; }

        public IList<HRworksConnector.Models.PersonEndpoints.WorkHours> WorkingHours { get; set; }

        public PersonWorkingHours()
        {
            this.WorkingHours = new System.Collections.Generic.List<HRworksConnector.Models.PersonEndpoints.WorkHours>();
        }
    }

    public class WorkHours
    {
        public DateTimeOffset BeginDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public long WorkingHours { get; set; }
    }

    public class GetAvailableWorkingHoursConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GetAvailableWorkingHoursResponse);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Newtonsoft.Json.Linq.JObject getAvailableWorkingHoursResponseAsJson = Newtonsoft.Json.Linq.JObject.Load(reader);

            GetAvailableWorkingHoursResponse getAvailableWorkingHoursResponse = new GetAvailableWorkingHoursResponse();

            foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> personWorkHours in getAvailableWorkingHoursResponseAsJson)
            {
                string personId = personWorkHours.Key;
                Newtonsoft.Json.Linq.JArray workHours = personWorkHours.Value as Newtonsoft.Json.Linq.JArray;


                PersonWorkingHours tmpPersonWorkingHours = new PersonWorkingHours();
                tmpPersonWorkingHours.OrganizationUnitNumber = personId;

                foreach (Newtonsoft.Json.Linq.JToken workHour in workHours)
                {
                    HRworksConnector.Models.PersonEndpoints.WorkHours tmpWorkHours = workHour.ToObject<HRworksConnector.Models.PersonEndpoints.WorkHours>();
                    tmpPersonWorkingHours.WorkingHours.Add(tmpWorkHours);
                }

                getAvailableWorkingHoursResponse.PersonsWorkingHours.Add(tmpPersonWorkingHours);
            }

            return getAvailableWorkingHoursResponse;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}