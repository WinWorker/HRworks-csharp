using System;

namespace HRworksConnector
{
    class Program
    {
        const string AccessKey = @"mein AccessKey";
        const string SecretAccessKey = @"mein SecretKey";

        static void Main(string[] args)
        {
            try
            {
                System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(async delegate
                {
                    try
                    {
                        await CallEndpoints();
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

        private static async System.Threading.Tasks.Task CallEndpoints()
        {
            try
            {
                HRworksConnector.HRworksApi hrworksApi = new HRworksConnector.HRworksApi(AccessKey, SecretAccessKey);

                #region GetAllPermanentEstablishments

                HRworksConnector.Models.GeneralEndpoints.PermanentEstablishmentsResponse permanentEstablishmentsResponse = await hrworksApi.GeneralEndpoints.GetAllPermanentEstablishmentsAsync();

                #endregion

                #region Target GetHolidays

                HRworksConnector.Models.GeneralEndpoints.HolidaysResponse holidaysResponse = await hrworksApi.GeneralEndpoints.GetHolidaysAsync(System.DateTime.Now.Year);

                #endregion

                #region Target GetAllAbsenceTypes / GetSickLeaveTypes

                // HRworksConnector.Models.AbsenceEndpoints.GetAllAbsenceTypesResponse getAllAbsenceTypesResponse = await hrworksApi.AbsenceEndpoints.GetAbsenceTypesAsync();

                // HRworksConnector.Models.AbsenceEndpoints.GetSickLeaveTypesResponse getSickLeaveTypesResponse = await hrworksApi.AbsenceEndpoints.GetSickLeaveTypesAsync();

                HRworksConnector.Models.AbsenceEndpoints.AbsenceBaseTypeCollection absenceAndSickLeaveTypes = await hrworksApi.AbsenceEndpoints.GetAbsenceAndSickLeaveTypesAsync();

                #endregion

                #region Target GetPersons

                HRworksConnector.Models.PersonEndpoints.GetPersonsResponse persons = await hrworksApi.PersonEndpoints.GetPersonsAsync();

                #endregion

                #region GetAvailableWorkingHours

                System.DateTime today = System.DateTime.Now.Date;

                System.DateTime dateFrom = today.Date.AddDays(-60);
                System.DateTime dateTo = today.AddDays(60);

                HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursRequest getAvailableWorkingHoursRequest = new HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursRequest();
                getAvailableWorkingHoursRequest.BeginDate = dateFrom;
                getAvailableWorkingHoursRequest.EndDate = dateTo;

                int counter = 0;

                HRworksConnector.Models.PersonEndpoints.PersonBaseData[] personsAsArray = persons.ToPersonArray();

                foreach (HRworksConnector.Models.PersonEndpoints.PersonBaseData personBaseData in personsAsArray)
                {
                    counter++;
                    getAvailableWorkingHoursRequest.PersonnelNumbers.Add(personBaseData.PersonnelNumber);

                    if (counter > 30)
                    {
                        break;
                    }
                }

                HRworksConnector.Models.PersonEndpoints.GetAvailableWorkingHoursResponse getAvailableWorkingHoursResponse = await hrworksApi.PersonEndpoints.GetAvailableWorkingHoursAsync(getAvailableWorkingHoursRequest);

                #endregion

                #region Target GetAbsences / SickLeaves

                string[] activeKrankAndUrlaubKeys = absenceAndSickLeaveTypes.GetActiveKrankAndUrlaubKeys();

                // Krank- und Urlaubsmeldungen holen
                foreach (HRworksConnector.Models.PersonEndpoints.PersonBaseData personBaseData in persons.ToPersonArray())
                {
                    HRworksConnector.Models.AbsenceEndpoints.GetAbsencesRequest getAbsencesRequest = new HRworksConnector.Models.AbsenceEndpoints.GetAbsencesRequest();
                    getAbsencesRequest.BeginDate = dateFrom;
                    getAbsencesRequest.EndDate = dateTo;
                    getAbsencesRequest.PersonnelNumbers.Add(personBaseData.PersonnelNumber);
                    foreach (string activeKrankAndUrlaubKey in activeKrankAndUrlaubKeys)
                    {
                        getAbsencesRequest.Types.Add(activeKrankAndUrlaubKey);
                    }

                    HRworksConnector.Models.AbsenceEndpoints.GetAbsencesResponse getAbsencesResponse = await hrworksApi.AbsenceEndpoints.GetAbsencesAsync(getAbsencesRequest);

                    HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesRequest getSickLeavesRequest = new HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesRequest();
                    getSickLeavesRequest.BeginDate = dateFrom;
                    getSickLeavesRequest.EndDate = dateTo;
                    getSickLeavesRequest.PersonnelNumbers.Add(personBaseData.PersonnelNumber);
                    foreach (string activeKrankAndUrlaubKey in activeKrankAndUrlaubKeys)
                    {
                        getSickLeavesRequest.Types.Add(activeKrankAndUrlaubKey);
                    }

                    HRworksConnector.Models.AbsenceEndpoints.GetSickLeavesResponse getSickLeavesResponse = await hrworksApi.AbsenceEndpoints.GetSickLeavesAsync(getSickLeavesRequest);
                }

                #endregion

                #region Target GetPersonMasterData

                string personnelNumber = string.Empty;

                if (persons.OrganizationsPersons.Count > 0)
                {
                    System.Random random = new System.Random();
                    int randPos = random.Next(persons.OrganizationsPersons.Count);
                    HRworksConnector.Models.PersonEndpoints.OrganizationPersons organizationPersons = persons.OrganizationsPersons[randPos];

                    randPos = random.Next(organizationPersons.Persons.Count);

                    HRworksConnector.Models.PersonEndpoints.PersonBaseData tmPersonBaseData = organizationPersons.Persons[randPos];
                    personnelNumber = tmPersonBaseData.PersonnelNumber;
                }

                HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataRequest getPersonMasterDataRequest = new HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataRequest();
                getPersonMasterDataRequest.PersonnelNumbers.Add(personnelNumber);
                HRworksConnector.Models.PersonEndpoints.GetPersonMasterDataResponse getPersonMasterDataResponse = await hrworksApi.PersonEndpoints.GetPersonMasterDataAsync(getPersonMasterDataRequest);

                #endregion

                #region Target GetAllOrganizationUnits

                HRworksConnector.Models.GeneralEndpoints.OrganizationUnitsResponse allOrganizationUnits = await hrworksApi.GeneralEndpoints.GetAllOrganizationUnitsAsync();

                System.Console.WriteLine("-- GetAllOrganizationUnits-- ");
                foreach (HRworksConnector.Models.GeneralEndpoints.OrganizationUnit organizationUnit in allOrganizationUnits.OrganizationUnits)
                {
                    System.Console.WriteLine(organizationUnit.ToString());

                    // HRworksConnector.Models.GeneralEndpoints.OrganizationUnit tmpOrganizationUnit = await hrworksApi.GeneralEndpoints.GetOrganizationUnitByNumberAsync(organizationUnit.Number);
                }

                #endregion

                #region Target GetPresentPersonsOfOrganizationUnit

                if (allOrganizationUnits.OrganizationUnits.Count == 0)
                {
                    return;
                }

                string tmpOrganizationUnitNumber = allOrganizationUnits.OrganizationUnits[0].Number;
                string tmpOrganizationUnitName = allOrganizationUnits.OrganizationUnits[0].Name;

                System.Console.WriteLine();
                System.Console.WriteLine();

                // Target GetPresentPersonsOfOrganizationUnit
                HRworksConnector.Models.GeneralEndpoints.PersonsOfOrganizationUnitResponse personsOfOrganizationUnitResponse = await hrworksApi.GeneralEndpoints.GetPresentPersonsOfOrganizationUnitAsync(tmpOrganizationUnitNumber);

                System.Console.WriteLine(string.Format("-- GetPresentPersonsOfOrganizationUnit von {0} ({1}) -- ", tmpOrganizationUnitName, tmpOrganizationUnitNumber));
                foreach (HRworksConnector.Models.PersonEndpoints.PersonBaseData personBaseData in personsOfOrganizationUnitResponse.Persons)
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