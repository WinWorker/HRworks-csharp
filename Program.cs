using System;

namespace HRworksConnector
{
    class Program
    {
        const string AccessKey = @"mein AccessKey";
        const string SecretAccessKey = @"mein SecretKey";
        const string RealmIdentifier = "production";

        static void Main(string[] args)
        {
            try
            {
                System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(async delegate
                {
                    try
                    {
                        await CallActions();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                });

                task.Wait();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            System.Console.WriteLine();
            System.Console.Write("Drücken Sie eine Taste, um das Progamm zu beenden.");
            System.Console.ReadKey();
        }

        private static async System.Threading.Tasks.Task CallActions()
        {
            try
            {
                HRworksConnector.HRworksApi hrworksApi = new HRworksConnector.HRworksApi(AccessKey, SecretAccessKey, RealmIdentifier);

                #region GetAllPermanentEstablishments

                HRworksConnector.Models.GeneralActions.PermanentEstablishmentsResponse permanentEstablishmentsResponse = await hrworksApi.GeneralActions.GetAllPermanentEstablishmentsAsync();

                #endregion

                #region Target GetHolidays

                HRworksConnector.Models.GeneralActions.HolidaysResponse holidaysResponse = await hrworksApi.GeneralActions.GetHolidaysAsync(System.DateTime.Now.Year);

                #endregion

                #region Target GetAllAbsenceTypes

                HRworksConnector.Models.AbsenceActions.GetAllAbsenceTypesResponse getAllAbsenceTypesResponse = await hrworksApi.AbsenceActions.GetAllAbsenceTypesAsync();

                #endregion

                #region Target GetPersons

                HRworksConnector.Models.GeneralActions.GetPersonsResponse persons = await hrworksApi.GeneralActions.GetPersonsAsync();

                #endregion

                #region GetAvailableWorkingHours

                System.DateTime today = System.DateTime.Now.Date;

                System.DateTime dateFrom = today.Date.AddDays(-60);
                System.DateTime dateTo = today.AddDays(60);

                HRworksConnector.Models.GeneralActions.GetAvailableWorkingHoursRequest getAvailableWorkingHoursRequest = new HRworksConnector.Models.GeneralActions.GetAvailableWorkingHoursRequest();
                getAvailableWorkingHoursRequest.BeginDate = dateFrom;
                getAvailableWorkingHoursRequest.EndDate = dateTo;

                int counter = 0;

                HRworksConnector.Models.GeneralActions.PersonBaseData[] personsAsArray = persons.ToPersonArray();

                foreach (HRworksConnector.Models.GeneralActions.PersonBaseData personBaseData in personsAsArray)
                {
                    counter++;
                    getAvailableWorkingHoursRequest.PersonnelNumbers.Add(personBaseData.PersonnelNumber);

                    if (counter > 30)
                    {
                        break;
                    }
                }

                HRworksConnector.Models.GeneralActions.GetAvailableWorkingHoursResponse getAvailableWorkingHoursResponse = await hrworksApi.GeneralActions.GetAvailableWorkingHoursAsync(getAvailableWorkingHoursRequest);

                #endregion

                #region Target GetAbsences

                string[] activeKrankAndUrlaubKeys = getAllAbsenceTypesResponse.GetActiveKrankAndUrlaubKeys();

                // Krank- und Urlaubsmeldungen holen
                foreach (HRworksConnector.Models.GeneralActions.PersonBaseData personBaseData in persons.ToPersonArray())
                {
                    HRworksConnector.Models.AbsenceActions.GetAbsencesRequest getAbsencesRequest = new HRworksConnector.Models.AbsenceActions.GetAbsencesRequest();
                    getAbsencesRequest.BeginDate = dateFrom;
                    getAbsencesRequest.EndDate = dateTo;
                    getAbsencesRequest.PersonnelNumbers.Add(personBaseData.PersonnelNumber);
                    foreach (string activeKrankAndUrlaubKey in activeKrankAndUrlaubKeys)
                    {
                        getAbsencesRequest.Types.Add(activeKrankAndUrlaubKey);
                    }

                    HRworksConnector.Models.AbsenceActions.GetAbsencesResponse getAbsencesResponse = await hrworksApi.AbsenceActions.GetAbsencesAsync(getAbsencesRequest);
                }

                #endregion

                #region Target GetPersonMasterData

                string personnelNumber = string.Empty;

                if (persons.OrganizationsPersons.Count > 0)
                {
                    System.Random random = new System.Random();
                    int randPos = random.Next(persons.OrganizationsPersons.Count - 1);
                    HRworksConnector.Models.GeneralActions.OrganizationPersons organizationPersons = persons.OrganizationsPersons[randPos];

                    randPos = random.Next(organizationPersons.Persons.Count - 1);

                    HRworksConnector.Models.GeneralActions.PersonBaseData tmPersonBaseData = organizationPersons.Persons[randPos];
                    personnelNumber = tmPersonBaseData.PersonnelNumber;
                }

                HRworksConnector.Models.EmployeeActions.GetPersonMasterDataRequest getPersonMasterDataRequest = new HRworksConnector.Models.EmployeeActions.GetPersonMasterDataRequest();
                getPersonMasterDataRequest.PersonnelNumbers.Add(personnelNumber);
                HRworksConnector.Models.EmployeeActions.GetPersonMasterDataResponse getPersonMasterDataResponse = await hrworksApi.EmployeeActions.GetPersonMasterDataAsync(getPersonMasterDataRequest);

                #endregion

                #region Target GetAllOrganizationUnits

                HRworksConnector.Models.GeneralActions.OrganizationUnitsResponse allOrganizationUnits = await hrworksApi.GeneralActions.GetAllOrganizationUnitsAsync();

                System.Console.WriteLine("-- GetAllOrganizationUnits-- ");
                foreach (HRworksConnector.Models.GeneralActions.OrganizationUnit organizationUnit in allOrganizationUnits.OrganizationUnits)
                {
                    System.Console.WriteLine(organizationUnit.ToString());
                }

                #endregion

                #region Target GetPresentPersonsOfOrganizationUnit

                if (allOrganizationUnits.OrganizationUnits.Count == 0)
                {
                    return;
                }

                string tmpOrganizationUnitNumber = allOrganizationUnits.OrganizationUnits[0].OrganizationUnitNumber;
                string tmpOrganizationUnitName = allOrganizationUnits.OrganizationUnits[0].OrganizationUnitName;

                System.Console.WriteLine();
                System.Console.WriteLine();

                // Target GetPresentPersonsOfOrganizationUnit
                HRworksConnector.Models.GeneralActions.PersonsOfOrganizationUnitResponse personsOfOrganizationUnitResponse = await hrworksApi.GeneralActions.GetPresentPersonsOfOrganizationUnitAsync(tmpOrganizationUnitNumber);

                System.Console.WriteLine(string.Format("-- GetPresentPersonsOfOrganizationUnit von {0} ({1}) -- ", tmpOrganizationUnitName, tmpOrganizationUnitNumber));
                foreach (HRworksConnector.Models.GeneralActions.PersonBaseData personBaseData in personsOfOrganizationUnitResponse.Persons)
                {
                    System.Console.WriteLine(personBaseData.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}