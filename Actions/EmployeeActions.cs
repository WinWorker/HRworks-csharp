using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Actions
{
    public interface IEmployeeActions : HRworksConnector.IBase
    {
        /// <summary>
        /// Returns the current master data of the specified persons.
        /// </summary>
        /// <param name="getPersonMasterDataRequest"></param>
        /// <returns>A collection of Person objects</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.EmployeeActions.GetPersonMasterDataResponse> GetPersonMasterDataAsync(HRworksConnector.Models.EmployeeActions.GetPersonMasterDataRequest getPersonMasterDataRequest);
    }

    public class EmployeeActions : HRworksConnector.Base, IEmployeeActions
    {
        #region Constructor

        public EmployeeActions(string accessKey, string secretAccessKey, string realmIdentifier) : base(accessKey, secretAccessKey, realmIdentifier)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the current master data of the specified persons.
        /// </summary>
        /// <param name="getPersonMasterDataRequest"></param>
        /// <returns>A collection of Person objects</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.EmployeeActions.GetPersonMasterDataResponse> GetPersonMasterDataAsync(HRworksConnector.Models.EmployeeActions.GetPersonMasterDataRequest getPersonMasterDataRequest)
        {
            const string Target = @"GetPersonMasterData";

            Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.FromObject(getPersonMasterDataRequest);

            return await PostAsync<HRworksConnector.Models.EmployeeActions.GetPersonMasterDataResponse>(Target, json);
        }

        #endregion
    }
}
