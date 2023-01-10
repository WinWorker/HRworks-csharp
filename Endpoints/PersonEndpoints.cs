using HRworksConnector.Models.PersonEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Endpoints
{
    public interface IPersonEndpoints : HRworksConnector.IBase
    {
        /// <summary>
        /// Lists all persons in the company (or in the specified organization units). By default, only active
        /// persons are returned.Each person that was neither deleted nor has left the company counts as
        /// active.
        /// </summary>
        /// <returns>A collection of PersonBaseData objects, grouped by organization unit identifier.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.PersonEndpoints.GetPersonsResponse> GetPersonsAsync();

        /// <summary>
        /// Returns the cumulated available working hours of the specified persons in the date interval
        /// specified by the beginDate and endDate parameters.The selected date interval can be further
        /// divided into days, weeks or months.
        /// </summary>
        /// <returns>A collection of (sub) date intervals for each person identifier that includes the cumulated working hours value for the respective date interval.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursResponse> GetAvailableWorkingHoursAsync(HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursRequest getAvailableWorkingHoursRequest);

        /// <summary>
        /// Returns the current master data of the specified persons.
        /// </summary>
        /// <param name="getPersonMasterDataRequest"></param>
        /// <returns>A collection of Person objects</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataResponse> GetPersonMasterDataAsync(HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataRequest getPersonMasterDataRequest);
    }

    public class PersonEndpoints : HRworksConnector.Base, IPersonEndpoints
    {
        #region Constructor

        public PersonEndpoints(string accessKey, string secretAccessKey) : base(accessKey, secretAccessKey)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Lists all persons in the company (or in the specified organization units). By default, only active
        /// persons are returned.Each person that was neither deleted nor has left the company counts as
        /// active.
        /// </summary>
        /// <returns>A collection of PersonBaseData objects, grouped by organization unit identifier.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.PersonEndpoints.GetPersonsResponse> GetPersonsAsync()
        {
            const string Target = @"/v2/persons";
            return await GetAsync<HRworksConnector.Models.PersonEndpoints.GetPersonsResponse>(Target);
        }

        /// <summary>
        /// Returns the cumulated available working hours of the specified persons in the date interval
        /// specified by the beginDate and endDate parameters.The selected date interval can be further
        /// divided into days, weeks or months.
        /// </summary>
        /// <returns>A collection of (sub) date intervals for each person identifier that includes the cumulated working hours value for the respective date interval.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursResponse> GetAvailableWorkingHoursAsync(HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursRequest getAvailableWorkingHoursRequest)
        {
            const string Target = @"/v2/persons/available-working-hours";

            // Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.FromObject(getAvailableWorkingHoursRequest);

            System.Collections.Generic.List<string> urlParameters = new System.Collections.Generic.List<string>();
            urlParameters.Add(string.Format("beginDate={0}", getAvailableWorkingHoursRequest.BeginDate.ToString("yyyy-MM-dd")));
            urlParameters.Add(string.Format("endDate={0}", getAvailableWorkingHoursRequest.EndDate.ToString("yyyy-MM-dd")));
            urlParameters.Add("usePersonnelNumbers=true");

            string persons = string.Join(",", getAvailableWorkingHoursRequest.PersonnelNumbers.ToArray<string>());
            urlParameters.Add(string.Format("persons=[{0}]", persons));

            string queryString = string.Join("&", urlParameters.ToArray());

            return await GetAsync<HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursResponse>(Target, queryString);
        }

        /// <summary>
        /// Returns the current master data of the specified persons.
        /// </summary>
        /// <param name="getPersonMasterDataRequest"></param>
        /// <returns>A collection of Person objects</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataResponse> GetPersonMasterDataAsync(HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataRequest getPersonMasterDataRequest)
        {
            const string Target = @"/v2/persons/master-data";

            System.Collections.Generic.List<string> urlParameters = new System.Collections.Generic.List<string>();
            urlParameters.Add("usePersonnelNumbers=true");

            string persons = string.Join(",", getPersonMasterDataRequest.PersonnelNumbers.ToArray<string>());
            urlParameters.Add(string.Format("persons=[{0}]", persons));

            string queryString = string.Join("&", urlParameters.ToArray());

            return await GetAsync<HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataResponse>(Target, queryString);
        }

        #endregion
    }
}
