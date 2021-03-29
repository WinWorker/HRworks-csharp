using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Actions
{
    public interface IAbsenceActions : HRworksConnector.IBase
    {
        /// <summary>
        /// Lists all absence types defined for the caller’s customer account in HRworks. The keys returned by
        /// this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>A collection of absence type objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceActions.GetAllAbsenceTypesResponse> GetAllAbsenceTypesAsync();

        /// <summary>
        /// Returns a list of absences for the specified persons in the specified date interval.
        /// </summary>
        /// <returns>A collection of person identifiers mapped to the absences data for each person.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceActions.GetAbsencesResponse> GetAbsencesAsync(HRworksConnector.Models.AbsenceActions.GetAbsencesRequest getAbsencesRequest);
    }

    public class AbsenceActions : HRworksConnector.Base, IAbsenceActions
    {
        #region Constructor

        public AbsenceActions(string accessKey, string secretAccessKey, string realmIdentifier) : base(accessKey, secretAccessKey, realmIdentifier)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lists all absence types defined for the caller’s customer account in HRworks. The keys returned by
        /// this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>A collection of absence type objects.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceActions.GetAllAbsenceTypesResponse> GetAllAbsenceTypesAsync()
        {
            const string Target = @"GetAllAbsenceTypes";
            return await PostAsync<HRworksConnector.Models.AbsenceActions.GetAllAbsenceTypesResponse>(Target, "");
        }

        /// <summary>
        /// Returns a list of absences for the specified persons in the specified date interval.
        /// </summary>
        /// <returns>A collection of person identifiers mapped to the absences data for each person.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceActions.GetAbsencesResponse> GetAbsencesAsync(HRworksConnector.Models.AbsenceActions.GetAbsencesRequest getAbsencesRequest)
        {
            const string Target = @"GetAbsences";

            Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.FromObject(getAbsencesRequest);

            return await PostAsync<HRworksConnector.Models.AbsenceActions.GetAbsencesResponse>(Target, json);
        }

        #endregion
    }
}