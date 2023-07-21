using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.PersonEndpoints
{
    public partial class Person : HRworksConnector.Models.PersonEndpoints.PersonBaseData
    {
        [Newtonsoft.Json.JsonProperty("personLicenseNumber")]
        public string PersonLicenseNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("title")]
        public string Title { get; set; }

        [Newtonsoft.Json.JsonProperty("email")]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty("gender")]
        public string Gender { get; set; }

        [Newtonsoft.Json.JsonProperty("position")]
        public string Position { get; set; }

        [Newtonsoft.Json.JsonProperty("probationEndDate")]
        public System.DateTime ProbationEndDate { get; set; }

        [Newtonsoft.Json.JsonProperty("highestLevelOfEducation")]
        public string HighestLevelOfEducation { get; set; }

        [Newtonsoft.Json.JsonProperty("birthday")]
        public System.DateTime Birthday { get; set; }

        [Newtonsoft.Json.JsonProperty("taxpayerIdentificationNumber")]
        public string TaxpayerIdentificationNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("bankAccount")]
        public BankAccount BankAccount { get; set; }

        [Newtonsoft.Json.JsonProperty("Kürzel")]
        public string Kürzel { get; set; }

        [Newtonsoft.Json.JsonProperty("buildingOrRoom")]
        public string BuildingOrRoom { get; set; }

        [Newtonsoft.Json.JsonProperty("employmentType")]
        public string EmploymentType { get; set; }

        [Newtonsoft.Json.JsonProperty("costObject")]
        public Cost CostObject { get; set; }

        [Newtonsoft.Json.JsonProperty("permanentEstablishment")]
        public PermanentEstablishment PermanentEstablishment { get; set; }

        [Newtonsoft.Json.JsonProperty("Arbeitsverhältnis weitere")]
        public string ArbeitsverhältnisWeitere { get; set; }

        [Newtonsoft.Json.JsonProperty("address")]
        public Address Address { get; set; }

        [Newtonsoft.Json.JsonProperty("superior")]
        public Superior Superior { get; set; }

        [Newtonsoft.Json.JsonProperty("organizationUnit")]
        public HRworksConnector.Models.GeneralEndpoints.OrganizationUnit OrganizationUnit { get; set; }

        [Newtonsoft.Json.JsonProperty("joinDate")]
        public DateTimeOffset JoinDate { get; set; }

        [Newtonsoft.Json.JsonProperty("companyMobilePhoneNumber")]
        public string CompanyMobilePhoneNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("officePhoneNumber")]
        public string OfficePhoneNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("Bereich")]
        public string Bereich { get; set; }

        [Newtonsoft.Json.JsonProperty("socialSecurityNumber")]
        public string SocialSecurityNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [Newtonsoft.Json.JsonProperty("workSchedule")]
        public WorkSchedule WorkSchedule { get; set; }

        [Newtonsoft.Json.JsonProperty("costCentre")]
        public Cost CostCentre { get; set; }

        [Newtonsoft.Json.JsonProperty("nationality")]
        public string Nationality { get; set; }
    }

    public partial class Person
    {
        public override string ToString()
        {
            return string.Format("{0} - {1}, {2} ({3})", this.PersonId, this.LastName, this.FirstName, this.PersonnelNumber);
        }
    }
}