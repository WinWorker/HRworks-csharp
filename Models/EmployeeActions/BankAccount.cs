using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.EmployeeActions
{
    public partial class BankAccount
    {
        [Newtonsoft.Json.JsonProperty("bic")]
        public string Bic { get; set; }

        [Newtonsoft.Json.JsonProperty("iban")]
        public string Iban { get; set; }
    }
}