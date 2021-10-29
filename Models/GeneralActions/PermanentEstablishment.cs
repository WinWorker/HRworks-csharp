using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralActions
{
    public partial class PermanentEstablishment
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class PermanentEstablishment
    {
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.Id);
        }
    }
}