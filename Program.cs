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
            System.Threading.Tasks.Task task = System.Threading.Tasks.Task<string>.Run(async delegate
            {
                try
                {
                    HRworksConnector.HRworksApi hrworksApi = new HRworksConnector.HRworksApi(AccessKey, SecretAccessKey, RealmIdentifier);

                    #region Target GetAllAbsenceTypes

                    HRworksConnector.Models.AbsenceActions.GetAllAbsenceTypesResponse getAllAbsenceTypesResponse = await hrworksApi.AbsenceActions.GetAllAbsenceTypesAsync();

                    #endregion

                    #region Target GetPersons

                    HRworksConnector.Models.GeneralActions.GetPersonsResponse persons = await hrworksApi.GeneralActions.GetPersonsAsync();

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

                    #region Target GetAbsences

                    HRworksConnector.Models.AbsenceActions.GetAbsencesRequest getAbsencesRequest = new HRworksConnector.Models.AbsenceActions.GetAbsencesRequest();
                    getAbsencesRequest.BeginDate = System.DateTime.Now.AddDays(-30);
                    getAbsencesRequest.EndDate = new System.DateTime(System.DateTime.Now.Year, 12, 31);
                    getAbsencesRequest.PersonnelNumbers.Add(personnelNumber);
                    HRworksConnector.Models.AbsenceActions.GetAbsencesResponse getAbsencesResponse = await hrworksApi.AbsenceActions.GetAbsencesAsync(getAbsencesRequest);

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
            });

            task.Wait();

            System.Console.ReadLine();
        }
    }
}
