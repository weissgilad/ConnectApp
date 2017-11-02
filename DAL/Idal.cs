using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    /// <summary>
    /// interface for Data Access Layer.
    /// any BL wishing to use this layer can use these methods safely
    /// </summary>
    public interface Idal
    {

        //Specialty----------------------------------------------//
        /// <summary>
        /// checks if specialization id is valid and enters it into datasource
        /// if id is 0, one will be assigned.
        /// </summary>
        /// <exception cref="Id_Already_Registered_Exception"></exception>
        /// <exception cref="Invalid_Id_Exception">when id is not 0 or 8 digits</exception>
        /// <exception cref="File_Error_Exception"></exception>
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
        /// <param name="currentId"></param>
        /// <param name="New"></param>
        void EditSpecialty(int currentId, Specialization New);

        //Employee-----------------------------------------------//

        /// <summary>
        /// checks if employee id is valid and enters it into datasource
        /// </summary>
        /// <exception cref="Id_Already_Registered_Exception"></exception>
        /// <exception cref="Invalid_Id_Exception">when id is smaller than 1 or not int</exception>
        /// <exception cref="File_Error_Exception"></exception>
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
        /// <exception cref="Invalid_Id_Exception">when id is smaller than 1 or not int</exception>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="currentId">id of employee to be edited</param>
        /// <param name="New">employee containing new data</param>
        void EditEmployee(string currentId, Employee New);

        //Employer-----------------------------------------------//
        /// <summary>
        /// checks if employer id is valid and enters it into datasource
        /// </summary>
        /// <exception cref="Id_Already_Registered_Exception"></exception>
        /// <exception cref="Invalid_Id_Exception">when id smaller than 1 or not int</exception>
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
        /// <exception cref="Invalid_Id_Exception">when id is smaller than 1 or not int</exception>
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



    }
}
