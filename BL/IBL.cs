using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BL
{
    /// <summary>
    /// interface for Buisiness Layer,
    /// so any UI wishing to use this layer can access it properly.
    /// </summary>
    public interface IBL
    {
        //Specialty----------------------------------------------//
        /// <summary>
        /// checks if specialization id is valid and enters it into datasource
        /// if id is 0, one will be assigned.
        /// </summary>
        /// <exception cref="Id_Already_Registered_Exception"></exception>
        /// <exception cref="Invalid_Id_Exception">when id is not 0 or 8 digits</exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="minimum_More_Than_Maximum_Exception"></exception>
        /// <exception cref="Salary_Too_Low_Exception"></exception>
        /// <param name="New">specialization to be added</param>
        void AddSpecialty(Specialization New);
        /// <summary>
        /// removes specialty
        /// </summary>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="Id"></param>
        void RemoveSpecialty(int Id);
        /// <summary>
        /// edits a specialty in datasource
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="minimum_More_Than_Maximum_Exception"></exception>
        /// <exception cref="Salary_Too_Low_Exception"></exception>
        /// <param name="currentId"></param>
        /// <param name="New"></param>
        void EditSpecialty(int currentId, Specialization New);

        //Employee-----------------------------------------------//

        /// <summary>
        /// checks if employee id is valid and enters it into datasource
        /// </summary>
        /// <exception cref="Id_Already_Registered_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Candidate_Too_Young_Exception"></exception>
        /// <exception cref="Bank_Already_Exists_Exception"></exception>
        /// <exception cref="No_Such_Specialization_Exception"></exception>
        /// <exception cref="A_Number_Is_Negative_Or_Zero_Or_Not_Int_Exception"></exception>
        /// <param name="New">employee to be added</param>
        void AddEmployee(Employee New);
        /// <summary>
        /// removes employee
        /// </summary>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="Id"></param>
        void RemoveEmployee(string Id);
        /// <summary>
        /// replaces existing data with new data.
        /// </summary>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <exception cref="A_Number_Is_Negative_Or_Zero_Or_Not_Int_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Candidate_Too_Young_Exception"></exception>
        /// <exception cref="Bank_Already_Exists_Exception"></exception>
        /// <exception cref="No_Such_Specialization_Exception"></exception>
        /// <param name="currentId">id of employee to be edited</param>
        /// <param name="New">employee containing new data</param>
        void EditEmployee(string currentId, Employee New);

        //Employer-----------------------------------------------//
        /// <summary>
        /// checks if employer id is valid and enters it into datasource
        /// </summary>
        /// <exception cref="Id_Already_Registered_Exception"></exception>
        /// <exception cref="A_Number_Is_Negative_Or_Zero_Or_Not_Int_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="New">employer to be added</param>
        void AddEmployer(Employer New);
        /// <summary>
        /// removes employer
        /// </summary>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="Id"></param>
        void RemoveEmployer(string Id);
        /// <summary>
        /// replaces existing data with new data.
        /// </summary>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <exception cref="A_Number_Is_Negative_Or_Zero_Or_Not_Int_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="currentId">id of employer to be edited</param>
        /// <param name="New">employer containing new data</param>
        void EditEmployer(string currentId, Employer New);

        //Contract-----------------------------------------------//

        /// <summary>
        /// checks if contract id is valid and enters it into datasource
        /// if id is 0, one will be assigned.
        /// </summary>
        /// <exception cref="Id_Already_Registered_Exception"></exception>
        /// <exception cref="Invalid_Id_Exception">when id is not 0 or 8 digits</exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Company_Date_Error_Exception">
        /// if buisiness is less than a year old or future date is given
        /// </exception>
        /// <exception cref="Employee_And_Employer_Identical_Exception"></exception>
        /// <exception cref="Contract_Date_Problem_Exception"></exception>
        /// <exception cref="employee/employer_Dont_Exist_Exception"></exception>
        /// <exception cref="Work_Time_Invalid_Exception"></exception>
        /// <exception cref="Salary_Invalid_Exception"></exception>
        /// <param name="New">Contract to be added</param>
        void AddContract(Contract New);
        /// <summary>
        /// removes contract
        /// </summary>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="Id"></param>
        void RemoveContract(int Id);
        /// <summary>
        /// replaces existing data with new data.
        /// </summary>
        /// <exception cref="Invalid_Id_Exception"></exception>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Company_Date_Error_Exception">
        /// if buisiness is less than a year old or future date is given
        /// </exception>
        /// <exception cref="Employee_And_Employer_Identical_Exception"></exception>
        /// <exception cref="Contract_Date_Problem_Exception"></exception>
        /// <exception cref="employee/employer_Dont_Exist_Exception"></exception>
        /// <exception cref="Work_Time_Invalid_Exception"></exception>
        /// <exception cref="Salary_Invalid_Exception"></exception>
        /// <param name="currentId">id of contract to be edited</param>
        /// <param name="New">contract containing new data</param>
        void EditContract(int currentId, Contract New);

        //Get List-----------------------------------------------//
        /// <summary>
        /// gets specialization list
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns></returns>
        List<Specialization> GetSpecializationList();
        /// <summary>
        /// gets employee list
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>List of employees</returns>
        List<Employee> GetEmployeeList();
        /// <summary>
        /// gets employer list
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>List of employers</returns>
        List<Employer> GetEmployerList();
        /// <summary>
        /// gets contract list
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>List of contracts</returns>
        List<Contract> GetContractList();
        /// <summary>
        /// gets bank account info from the internet
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="WebClient_Exception"></exception>
        /// <returns>List of bank accounts</returns>
        List<BankAccount> GetAccountList();

        /// <summary>
        /// gets private employer list
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>List of private employers</returns>
        List<Employer> GetPrivateEmployerList();
        /// <summary>
        /// gets company employer list
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>List of company employers</returns>
        List<Employer> GetCompanyList();

        /// <summary>
        /// gets requested employee
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <returns></returns>
        Employee GetEmployee(string Id);
        /// <summary>
        /// gets requested employer
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <returns></returns>
        Employer GetEmployer(string Id);
        /// <summary>
        /// gets requested specialization
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <returns></returns>
        Specialization GetSpecialization(int Id);
        /// <summary>
        /// gets requested contract
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="Id_Not_Found_Exception"></exception>
        /// <returns></returns>
        Contract GetContract(int Id);

        //GroupBy-----------------------------------------------------//
        /// <summary>
        /// linq group employees by salary. will seperate by 1000s
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<int, Employee>> GroupBySalary(bool sort);
        /// <summary>
        /// linq group employees by age. will seperate by 10s
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<int, Employee>> GroupByAgeGroup(bool sort);
        /// <summary>
        /// linq group contracts by specialization area.
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<SpecType, Contract>> GroupBySpecializationArea(bool sort);
        /// <summary>
        /// linq group years by profits. will seperate by 100s
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<int, int>> GroupByYearlyProfit(bool sort);
        /// <summary>
        /// linq group profits by years.
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<int, int>> GroupProfitYearly(bool sort);
        /// <summary>
        /// linq group employers by specialization area.
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<SpecType, Employer>> GroupEmployerBySpecializationArea(bool sort);
        /// <summary>
        /// linq group employers by number of contracts employers have.
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<int, Employer>> GroupEmployerByNumContracts(bool sort);
        /// <summary>
        /// linq group specializations by the average gross wage their employees get. will seperate by 10s
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<int, Specialization>> GroupSpecializationByAverageGrossWage(bool sort);
        /// <summary>
        /// linq group specializations by number of people who have it.
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<int, Specialization>> GroupSpecializationByNumPeople(bool sort);
        /// <summary>
        /// linq group banks by city.
        /// and group that by bank name
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <exception cref="WebCient_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        IEnumerable<IGrouping<string, IGrouping<string, BankAccount>>> GroupBankByNameAndCity();

        void mail(string Sender, Contract JustSigned, string AddrEmper, string AddrEmpee);

    }
}
