using HRworksConnector.Models.PersonEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Endpoints
{
    public interface IAbsenceEndpoints : HRworksConnector.IBase
    {
        /// <summary>
        /// Lists all absence and all sick leave types defined for the caller’s customer account in HRworks. 
        /// The keys returned by this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>A collection of absence type objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseTypeCollection> GetAbsenceAndSickLeaveTypesAsync();

        /// <summary>
        /// Lists all absence types defined for the caller’s customer account in HRworks. 
        /// The keys returned by this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>A collection of absence type objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetAllAbsenceTypesResponse> GetAbsenceTypesAsync();

        /// <summary>
        /// Lists all sick leave types defined for the caller’s customer account in HRworks. The keys returned by this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>Return sick leave types</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetSickLeaveTypesResponse> GetSickLeaveTypesAsync();

        /// <summary>
        /// Returns a list of absences for the specified persons in the specified date interval.
        /// </summary>
        /// <returns>A collection of person identifiers mapped to the absences data for each person.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetAbsencesResponse> GetAbsencesAsync(HRworksConnector.Models.AbsenceEndpoints.GetAbsencesRequest absencesRequest);

        /// <summary>
        /// Returns a list of sick leaves for the specified persons in the specified date interval.
        /// </summary>
        /// <param name="getAbsencesRequest"></param>
        /// <returns>Return sick leaves.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesResponse> GetSickLeavesAsync(HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesRequest sickLeavesRequest);
    }

    public class AbsenceEndpoints : HRworksConnector.Base, IAbsenceEndpoints
    {
        #region Constructor

        public AbsenceEndpoints(string accessKey, string secretAccessKey) : base(accessKey, secretAccessKey)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lists all absence and all sick leave types defined for the caller’s customer account in HRworks. 
        /// The keys returned by this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>A collection of absence type objects.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseTypeCollection> GetAbsenceAndSickLeaveTypesAsync()
        {
            HRworksConnector.Models.AbsenceEndpoints.GetAllAbsenceTypesResponse getAllAbsenceTypesResponse = await GetAbsenceTypesAsync();
            HRworksConnector.Models.AbsenceEndpoints.GetSickLeaveTypesResponse getSickLeaveTypesResponse = await GetSickLeaveTypesAsync();

            HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseTypeCollection absenceAndSickLeaveTypes = new HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseTypeCollection();

            if (getAllAbsenceTypesResponse != null &&
                getAllAbsenceTypesResponse.AbsenceTypes != null)
            {
                foreach (HRworksConnector.Models.AbsenceEndpoints.AbsenceType absenceType in getAllAbsenceTypesResponse.AbsenceTypes)
                {
                    absenceAndSickLeaveTypes.Add(absenceType);
                }
            }

            if (getSickLeaveTypesResponse != null &&
                getSickLeaveTypesResponse.SickLeaveTypes != null)
            {
                foreach (HRworksConnector.Models.AbsenceEndpoints.SickLeaveType sickLeaveType in getSickLeaveTypesResponse.SickLeaveTypes)
                {
                    absenceAndSickLeaveTypes.Add(sickLeaveType);
                }
            }

            return absenceAndSickLeaveTypes;
        }

        /// <summary>
        /// Lists all absence types defined for the caller’s customer account in HRworks. The keys returned by
        /// this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>A collection of absence type objects.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetAllAbsenceTypesResponse> GetAbsenceTypesAsync()
        {
            const string Target = @"/v2/absences/absence-types";
            return await GetAsync<HRworksConnector.Models.AbsenceEndpoints.GetAllAbsenceTypesResponse>(Target);
        }

        /// <summary>
        /// Lists all sick leave types defined for the caller’s customer account in HRworks. The keys returned by this action can be used as filter values for other absence actions.
        /// </summary>
        /// <returns>Return sick leave types</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetSickLeaveTypesResponse> GetSickLeaveTypesAsync()
        {
            const string Target = @"/v2/sick-leaves/sick-leave-types";
            return await GetAsync<HRworksConnector.Models.AbsenceEndpoints.GetSickLeaveTypesResponse>(Target);
        }

        /// <summary>
        /// Returns a list of absences for the specified persons in the specified date interval.
        /// </summary>
        /// <returns>A collection of person identifiers mapped to the absences data for each person.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetAbsencesResponse> GetAbsencesAsync(HRworksConnector.Models.AbsenceEndpoints.GetAbsencesRequest absencesRequest)
        {
            const string Target = @"/v2/absences";

            System.Collections.Generic.List<string> urlParameters = new System.Collections.Generic.List<string>();
            urlParameters.Add(string.Format("beginDate={0}", absencesRequest.BeginDate.ToString("yyyy-MM-dd")));
            urlParameters.Add(string.Format("endDate={0}", absencesRequest.EndDate.ToString("yyyy-MM-dd")));
            urlParameters.Add("usePersonnelNumbers=true");

            string persons = string.Join(",", absencesRequest.PersonnelNumbers.ToArray<string>());
            urlParameters.Add(string.Format("persons=[{0}]", persons));

            string types = string.Join(",", absencesRequest.Types.ToArray<string>());
            urlParameters.Add(string.Format("types=[{0}]", types));

            string queryString = string.Join("&", urlParameters.ToArray());

            return await GetAsync<HRworksConnector.Models.AbsenceEndpoints.GetAbsencesResponse>(Target, queryString);
        }

        /// <summary>
        /// Returns a list of sick leaves for the specified persons in the specified date interval.
        /// </summary>
        /// <returns>Return sick leaves.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesResponse> GetSickLeavesAsync(HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesRequest sickLeavesRequest)
        {
            const string Target = @"/v2/sick-leaves";

            System.Collections.Generic.List<string> urlParameters = new System.Collections.Generic.List<string>();
            urlParameters.Add(string.Format("beginDate={0}", sickLeavesRequest.BeginDate.ToString("yyyy-MM-dd")));
            urlParameters.Add(string.Format("endDate={0}", sickLeavesRequest.EndDate.ToString("yyyy-MM-dd")));
            urlParameters.Add("usePersonnelNumbers=true");

            string persons = string.Join(",", sickLeavesRequest.PersonnelNumbers.ToArray<string>());
            urlParameters.Add(string.Format("persons=[{0}]", persons));

            string types = string.Join(",", sickLeavesRequest.Types.ToArray<string>());
            urlParameters.Add(string.Format("types=[{0}]", types));

            string queryString = string.Join("&", urlParameters.ToArray());

            return await GetAsync<HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesResponse>(Target, queryString);
        }

        #endregion
    }
}