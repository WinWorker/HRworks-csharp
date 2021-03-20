using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Utils.Converter
{
    public class DefaultDateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    {
        public DefaultDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
