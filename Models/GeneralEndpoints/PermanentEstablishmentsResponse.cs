using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Models.GeneralEndpoints
{
    public partial class PermanentEstablishmentsResponse
    {
        [Newtonsoft.Json.JsonProperty("permanentEstablishments")]
        public IList<HRworksConnector.Models.GeneralEndpoints.PermanentEstablishment> PermanentEstablishments { get; set; }
    }

    public partial class PermanentEstablishmentsResponse
    {
        public override string ToString()
        {
            return string.Format("{0} PermanentEstablishments", this.PermanentEstablishments.Count);
        }
    }
}