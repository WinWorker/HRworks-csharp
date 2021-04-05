using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralActions
{
    [Newtonsoft.Json.JsonConverter(typeof(HolidaysConverter))]
    public partial class HolidaysResponse
    {
        public System.Collections.Generic.IDictionary<string, Holidays>  Holidays { get; set; }

        public HolidaysResponse()
        {
            this.Holidays = new System.Collections.Generic.Dictionary<string, Holidays>();
        }
    }

    public class HolidaysConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(HolidaysResponse);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Newtonsoft.Json.Linq.JObject holidaysResponseAsJson = Newtonsoft.Json.Linq.JObject.Load(reader);

            HolidaysResponse holidaysResponse = new HolidaysResponse();

            foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> countryHolidays in holidaysResponseAsJson)
            {
                string countryCode = countryHolidays.Key;
                HRworksConnector.Models.GeneralActions.Holidays tmpCountryHolidays = countryHolidays.Value.ToObject<HRworksConnector.Models.GeneralActions.Holidays>();
                holidaysResponse.Holidays.Add(countryCode, tmpCountryHolidays);
            }

            return holidaysResponse;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
