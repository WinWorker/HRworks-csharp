using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Actions
{
    public interface IGeneralActions : HRworksConnector.IBase
    {
        /// <summary>
        /// Lists all organization units of the company with ID/number and name. Only active organization
        /// units will be returned
        /// </summary>
        /// <returns>A collection of OrganizationUnit objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralActions.OrganizationUnitsResponse> GetAllOrganizationUnitsAsync();

        /// <summary>
        /// Lists all persons that are currently ‘in the office’. This action does account for vacations, trips,
        /// sicknesses and other absences by forenoon and afternoon.A half-day absence is considered to
        /// last from 12 pm to 11:59 am, which counts as ‘forenoon’, likewise, an ‘afternoon’ absence is
        /// considered to last from 12 am to 11:59 pm.
        /// </summary>
        /// <param name="organizationUnitNumber"></param>
        /// <returns>A collection of PersonBaseData objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralActions.PersonsOfOrganizationUnitResponse> GetPresentPersonsOfOrganizationUnitAsync(string organizationUnitNumber);

        /// <summary>
        /// Lists all persons in the company (or in the specified organization units). By default, only active
        /// persons are returned.Each person that was neither deleted nor has left the company counts as
        /// active.
        /// </summary>
        /// <returns>A collection of PersonBaseData objects, grouped by organization unit identifier.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralActions.GetPersonsResponse> GetPersonsAsync();

        /// <summary>
        /// Lists the holidays for the company and its permanent establishments for the specified year( and country).
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        // System.Threading.Tasks.Task<string> GetHolidays(int year);
    }

    public class GenaralActions : HRworksConnector.Base, IGeneralActions
    {
        #region Constructor

        public GenaralActions(string accessKey, string secretAccessKey, string realmIdentifier) : base(accessKey, secretAccessKey, realmIdentifier)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lists all organization units of the company with ID/number and name. Only active organization
        /// units will be returned
        /// </summary>
        /// <returns>A collection of OrganizationUnit objects.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralActions.OrganizationUnitsResponse> GetAllOrganizationUnitsAsync()
        {
            const string Target = @"GetAllOrganizationUnits";
            return await PostAsync<HRworksConnector.Models.GeneralActions.OrganizationUnitsResponse>(Target, "");
        }

        /// <summary>
        /// Lists all persons that are currently ‘in the office’. This action does account for vacations, trips,
        /// sicknesses and other absences by forenoon and afternoon.A half-day absence is considered to
        /// last from 12 pm to 11:59 am, which counts as ‘forenoon’, likewise, an ‘afternoon’ absence is
        /// considered to last from 12 am to 11:59 pm.
        /// </summary>
        /// <param name="organizationUnitNumber"></param>
        /// <returns>A collection of PersonBaseData objects.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralActions.PersonsOfOrganizationUnitResponse> GetPresentPersonsOfOrganizationUnitAsync(string organizationUnitNumber)
        {
            Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject();
            json["organizationUnitNumber"] = organizationUnitNumber;

            const string Target = @"GetPresentPersonsOfOrganizationUnit";
            return await PostAsync<HRworksConnector.Models.GeneralActions.PersonsOfOrganizationUnitResponse>(Target, json);
        }

        /// <summary>
        /// Lists all persons in the company (or in the specified organization units). By default, only active
        /// persons are returned.Each person that was neither deleted nor has left the company counts as
        /// active.
        /// </summary>
        /// <returns>A collection of PersonBaseData objects, grouped by organization unit identifier.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralActions.GetPersonsResponse> GetPersonsAsync()
        {
            const string Target = @"GetPersons";
            return await PostAsync<HRworksConnector.Models.GeneralActions.GetPersonsResponse>(Target, "");
        }

        /// <summary>
        /// Lists the holidays for the company and its permanent establishments for the specified year( and country).
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        //public async System.Threading.Tasks.Task<string> GetHolidays(int year)
        //{
        //    const string Target = @"GetHolidays";

        //    Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject();
        //    json["year"] = year;

        //    Newtonsoft.Json.Linq.JArray countryCodesAsJson = new Newtonsoft.Json.Linq.JArray();
        //    countryCodesAsJson.Add("DEU");
        //    json["countryCodes"] = countryCodesAsJson;

        //    return await PostAsync<string>(Target, json);
        //}

        #endregion
    }
}