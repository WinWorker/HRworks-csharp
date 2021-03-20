using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.EmployeeActions
{
    public partial class Person : HRworksConnector.Models.GeneralActions.PersonBaseData
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
    }

    public partial class Person
    {
        public override string ToString()
        {
            return string.Format("{0} - {1}, {2} ({3})", this.PersonId, this.LastName, this.FirstName, this.PersonnelNumber);
        }
    }
}
