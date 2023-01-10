using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector
{
    public interface IHRworksApi
    {
        HRworksConnector.Endpoints.IGeneralEndpoints GeneralEndpoints { get; }
        HRworksConnector.Endpoints.IAbsenceEndpoints AbsenceEndpoints { get; }
        HRworksConnector.Endpoints.IPersonEndpoints PersonEndpoints { get; }
    }

    public class HRworksApi : IHRworksApi
    {
        #region Privates

        private string accessKey = string.Empty;
        private string secretAccessKey = string.Empty;

        #endregion

        #region Constructor

        public HRworksApi(string accessKey, string secretAccessKey)
        {
            this.accessKey = accessKey;
            this.secretAccessKey = secretAccessKey;

            this.GeneralEndpoints = new HRworksConnector.Endpoints.GenaralEndpoints(this.accessKey, this.secretAccessKey);
            this.AbsenceEndpoints = new HRworksConnector.Endpoints.AbsenceEndpoints(this.accessKey, this.secretAccessKey);
            this.PersonEndpoints = new HRworksConnector.Endpoints.PersonEndpoints(this.accessKey, this.secretAccessKey);
        }

        #endregion

        #region Properties

        public HRworksConnector.Endpoints.IGeneralEndpoints GeneralEndpoints { get; }

        public HRworksConnector.Endpoints.IAbsenceEndpoints AbsenceEndpoints { get; }

        public HRworksConnector.Endpoints.IPersonEndpoints PersonEndpoints { get; }

        #endregion

    }
}