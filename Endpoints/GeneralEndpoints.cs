using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRworksConnector.Endpoints
{
    public interface IGeneralEndpoints : HRworksConnector.IBase
    {
        /// <summary>
        /// Lists all organization units of the company with ID/number and name. Only active organization
        /// units will be returned
        /// </summary>
        /// <returns>A collection of OrganizationUnit objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.OrganizationUnitsResponse> GetAllOrganizationUnitsAsync();

        /// <summary>
        /// Return the data of a specific organization unit of the company by a given id.
        /// </summary>
        /// <returns>Return a single organization unit.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.OrganizationUnit> GetOrganizationUnitByNumberAsync(string organizationUnitNumber);

        /// <summary>
        /// Lists all persons that are currently ‘in the office’. This action does account for vacations, trips,
        /// sicknesses and other absences by forenoon and afternoon.A half-day absence is considered to
        /// last from 12 pm to 11:59 am, which counts as ‘forenoon’, likewise, an ‘afternoon’ absence is
        /// considered to last from 12 am to 11:59 pm.
        /// </summary>
        /// <param name="organizationUnitNumber"></param>
        /// <returns>A collection of PersonBaseData objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.PersonsOfOrganizationUnitResponse> GetPresentPersonsOfOrganizationUnitAsync(string organizationUnitNumber);

        /// <summary>
        /// Lists the holidays for the company and its permanent establishments for the specified year( and country).
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.HolidaysResponse> GetHolidaysAsync(int year);

        /// <summary>
        /// Lists all permanent establishments of the company with ID/number and name. Only active permanent establishments will be returned.
        /// </summary>
        /// <returns>A collection of PermanentEstablishment objects.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.PermanentEstablishmentsResponse> GetAllPermanentEstablishmentsAsync();

        /// <summary>
        /// Return the data of a specific permanent establishment of the company by a given id.
        /// </summary>
        /// <param name="id">ID of the permanent establishment to return data for.</param>
        /// <returns>Return a single permanent establishment.</returns>
        System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.PermanentEstablishment> GetPermanentEstablishmentByIdAsync(string id);
    }

    public class GenaralEndpoints : HRworksConnector.Base, IGeneralEndpoints
    {
        #region Constructor

        public GenaralEndpoints(string accessKey, string secretAccessKey) : base(accessKey, secretAccessKey)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lists all organization units of the company with ID/number and name. Only active organization
        /// units will be returned
        /// </summary>
        /// <returns>A collection of OrganizationUnit objects.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.OrganizationUnitsResponse> GetAllOrganizationUnitsAsync()
        {
            const string Target = @"/v2/organization-units";
            return await GetAsync<HRworksConnector.Models.GeneralEndpoints.OrganizationUnitsResponse>(Target);
        }

        /// <summary>
        /// Return the data of a specific organization unit of the company by a given id.
        /// </summary>
        /// <returns>Return a single organization unit.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.OrganizationUnit> GetOrganizationUnitByNumberAsync(string organizationUnitNumber)
        {
            string target = string.Format(@"/v2/organization-units/{0}", organizationUnitNumber);
            return await GetAsync<HRworksConnector.Models.GeneralEndpoints.OrganizationUnit>(target);
        }

        /// <summary>
        /// Lists all persons that are currently ‘in the office’. This action does account for vacations, trips,
        /// sicknesses and other absences by forenoon and afternoon.A half-day absence is considered to
        /// last from 12 pm to 11:59 am, which counts as ‘forenoon’, likewise, an ‘afternoon’ absence is
        /// considered to last from 12 am to 11:59 pm.
        /// </summary>
        /// <param name="organizationUnitNumber"></param>
        /// <returns>Get the currently present persons of the specified organization unit.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.PersonsOfOrganizationUnitResponse> GetPresentPersonsOfOrganizationUnitAsync(string organizationUnitNumber)
        {
            Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject();
            json["organizationUnitNumber"] = organizationUnitNumber;

            string target = string.Format(@"/v2/organization-units/{0}/present-persons", organizationUnitNumber);

            return await GetAsync<HRworksConnector.Models.GeneralEndpoints.PersonsOfOrganizationUnitResponse>(target);
        }

        /// <summary>
        /// Lists the holidays for the company and its permanent establishments for the specified year( and country).
        /// </summary>
        /// <param name="year"></param>
        /// <returns>A collection of holiday objects in JSON format, sorted by country codes and categorized into
        /// general holidays(for the whole country), state holidays(for specific regions or states) and
        /// permanent establishment holidays(that only affect certain permanent establishments of the company).</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.HolidaysResponse> GetHolidaysAsync(int year)
        {
            const string Target = @"/v2/holidays";

            System.Collections.Generic.List<string> urlParameters = new System.Collections.Generic.List<string>();
            urlParameters.Add(string.Format("year={0}", System.Net.WebUtility.UrlEncode(year.ToString())));
            urlParameters.Add(string.Format("countryCodes={0}", System.Net.WebUtility.UrlEncode("DEU")));

             string queryString = string.Join("&", urlParameters.ToArray());

            return await GetAsync<HRworksConnector.Models.GeneralEndpoints.HolidaysResponse>(Target, queryString);
        }

        /// <summary>
        /// Lists all permanent establishments of the company with ID/number and name. Only active permanent establishments will be returned.
        /// </summary>
        /// <returns>A collection of PermanentEstablishment objects.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.PermanentEstablishmentsResponse> GetAllPermanentEstablishmentsAsync()
        {
            const string Target = @"/v2/permanent-establishments";
            return await GetAsync<HRworksConnector.Models.GeneralEndpoints.PermanentEstablishmentsResponse>(Target);
        }

        /// <summary>
        /// Return the data of a specific permanent establishment of the company by a given id.
        /// </summary>
        /// <param name="id">ID of the permanent establishment to return data for.</param>
        /// <returns>Return a single permanent establishment.</returns>
        public async System.Threading.Tasks.Task<HRworksConnector.Models.GeneralEndpoints.PermanentEstablishment> GetPermanentEstablishmentByIdAsync(string id)
        {
            string target = string.Format(@"/v2/permanent-establishments/{0}", id);
            return await GetAsync<HRworksConnector.Models.GeneralEndpoints.PermanentEstablishment>(target);
        }

        #endregion
    }
}