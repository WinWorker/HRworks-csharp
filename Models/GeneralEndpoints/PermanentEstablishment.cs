namespace HRworksConnector.Models.GeneralEndpoints
{
    public partial class PermanentEstablishment
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("address")]
        public Address Address { get; set; }
    }

    public partial class PermanentEstablishment
    {
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.Id);
        }
    }

    public partial class Address
    {
        [Newtonsoft.Json.JsonProperty("street")]
        public string Street { get; set; }

        [Newtonsoft.Json.JsonProperty("additionalData")]
        public string AdditionalData { get; set; }

        [Newtonsoft.Json.JsonProperty("streetNumber")]
        public string StreetNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [Newtonsoft.Json.JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [Newtonsoft.Json.JsonProperty("city")]
        public string City { get; set; }
    }
}