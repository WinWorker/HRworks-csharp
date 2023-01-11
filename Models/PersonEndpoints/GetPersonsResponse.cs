using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.PersonEndpoints
{
    [Newtonsoft.Json.JsonConverter(typeof(GetPersonsConverter))]
    public partial class GetPersonsResponse
    {
        public IList<HRworksConnector.Models.PersonEndpoints.OrganizationPersons> OrganizationsPersons { get; set; }

        public GetPersonsResponse()
        {
            this.OrganizationsPersons = new System.Collections.Generic.List<HRworksConnector.Models.PersonEndpoints.OrganizationPersons>();
        }
    }

    public partial class GetPersonsResponse
    {
        public override string ToString()
        {
            return string.Format("{0} Organization Persons", this.OrganizationsPersons.Sum(op => op.Persons.Count));
        }

        /// <summary>
        /// Gibt alle Personen als platte Liste zurück.
        /// </summary>
        /// <returns></returns>
        public HRworksConnector.Models.PersonEndpoints.PersonBaseData[] ToPersonArray()
        {
            System.Collections.Generic.List<HRworksConnector.Models.PersonEndpoints.PersonBaseData> persons = new System.Collections.Generic.List<HRworksConnector.Models.PersonEndpoints.PersonBaseData>();

            foreach (HRworksConnector.Models.PersonEndpoints.OrganizationPersons organizationPersons in this.OrganizationsPersons)
            {
                foreach (HRworksConnector.Models.PersonEndpoints.PersonBaseData personBaseData in organizationPersons.Persons)
                {
                    persons.Add(personBaseData);
                }
            }

            return persons.ToArray();
        }
    }

    public class OrganizationPersons
    {
        public string OrganizationUnitNumber { get; set; }

        public IList<HRworksConnector.Models.PersonEndpoints.PersonBaseData> Persons { get; set; }

        public OrganizationPersons()
        {
            this.Persons = new System.Collections.Generic.List<HRworksConnector.Models.PersonEndpoints.PersonBaseData>();
        }
    }

    public class GetPersonsConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GetPersonsResponse);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Newtonsoft.Json.Linq.JObject getPersonsResponseAsJson = Newtonsoft.Json.Linq.JObject.Load(reader);

            GetPersonsResponse getPersonsResponse = new GetPersonsResponse();

            foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> organizationPersons in getPersonsResponseAsJson)
            {
                string organizationUnitNumber = organizationPersons.Key;
                Newtonsoft.Json.Linq.JArray persons = organizationPersons.Value as Newtonsoft.Json.Linq.JArray;


                OrganizationPersons tmpOrganizationPersons = new OrganizationPersons();
                tmpOrganizationPersons.OrganizationUnitNumber = organizationUnitNumber;

                foreach (Newtonsoft.Json.Linq.JToken person in persons)
                {
                    HRworksConnector.Models.PersonEndpoints.PersonBaseData personBaseData = person.ToObject<HRworksConnector.Models.PersonEndpoints.PersonBaseData>();
                    tmpOrganizationPersons.Persons.Add(personBaseData);
                }

                getPersonsResponse.OrganizationsPersons.Add(tmpOrganizationPersons);
            }

            return getPersonsResponse;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        /*
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            GetPersonsResponse getPersonsResponse = (GetPersonsResponse)value;

            Newtonsoft.Json.Linq.JArray organizationsPersons = new Newtonsoft.Json.Linq.JArray();

            foreach (HRworksConnector.Models.GeneralEndpoints.OrganizationPersons organizationPersons in getPersonsResponse.OrganizationsPersons)
            {
                Newtonsoft.Json.Linq.JObject organizationPersonsAsJson = new Newtonsoft.Json.Linq.JObject();

                Newtonsoft.Json.Linq.JArray persons = new Newtonsoft.Json.Linq.JArray();
                foreach (HRworksConnector.Models.GeneralEndpoints.PersonBaseData personBaseData in organizationPersons.Persons)
                {
                    Newtonsoft.Json.Linq.JObject personBaseDataAsJson = Newtonsoft.Json.Linq.JObject.FromObject(personBaseData);
                    persons.Add(personBaseDataAsJson);
                }

                organizationPersonsAsJson[organizationPersons.OrganizationUnitNumber] = persons;
            }

            organizationsPersons.WriteTo(writer);
        }
        */
    }
}