using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector
{
    public interface IHRworksApi
    {
        HRworksConnector.Actions.IGeneralActions GeneralActions { get; }
        HRworksConnector.Actions.IAbsenceActions AbsenceActions { get; }
        HRworksConnector.Actions.IEmployeeActions EmployeeActions { get; }
    }

    public class HRworksApi : IHRworksApi
    {
        #region Privates

        private string accessKey = string.Empty;
        private string secretAccessKey = string.Empty;
        private string realmIdentifier = string.Empty;

        #endregion

        #region Constructor

        public HRworksApi(string accessKey, string secretAccessKey, string realmIdentifier)
        {
            this.accessKey = accessKey;
            this.secretAccessKey = secretAccessKey;
            this.realmIdentifier = realmIdentifier;

            this.GeneralActions = new HRworksConnector.Actions.GenaralActions(this.accessKey, this.secretAccessKey, this.realmIdentifier);
            this.AbsenceActions = new HRworksConnector.Actions.AbsenceActions(this.accessKey, this.secretAccessKey, this.realmIdentifier);
            this.EmployeeActions = new HRworksConnector.Actions.EmployeeActions(this.accessKey, this.secretAccessKey, this.realmIdentifier);
        }

        #endregion

        #region Properties

        public HRworksConnector.Actions.IGeneralActions GeneralActions { get; }

        public HRworksConnector.Actions.IAbsenceActions AbsenceActions { get; }

        public HRworksConnector.Actions.IEmployeeActions EmployeeActions { get; }

        #endregion

    }
}