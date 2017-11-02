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
    /// Data Access Layer
    /// implements the Idal interface
    /// </summary>
    public sealed class Dal_imp : Idal
    {
        private static int ContractNum = 10000000;
        private static int Specnum = 10000000;

        internal Dal_imp()
        {

            //some example data
            object tmp;
            tmp = new Specialization()
            {
                MinimumSalary = 25,
                MaximumSalary = 35,
                SpecialtyName = "Apple Picking",
                Type = SpecType.Gardening
            };
            AddSpecialty(tmp as Specialization);

            tmp = new Specialization()
            {
                MinimumSalary = 100,
                MaximumSalary = 300,
                SpecialtyName = "Time Machine Repair",
                Type = SpecType.Paradoxical_Engineering
            };
            AddSpecialty(tmp as Specialization);

            tmp = new Employee()
            {
                Id = "123",
                LastName = "Cohen",
                FirstName = "Dan",
                Birthdate = new DateTime(1920, 1, 1),
                Address = "My House 1",
                Education = Degree.Student,
                Veteran = true,
                Account = new BankAccount(GetAccountList()[0], 4545),
                Specialty = 10000001,
                AccountNum = 4545,
                PhoneNumber = "05050505"
            };
            AddEmployee(tmp as Employee);
            tmp = new Employee()
            {
                Id = "333",
                LastName = "Cohen",
                FirstName = "Ron",
                Birthdate = new DateTime(1990, 1, 1),
                Address = "My House 1",
                Education = Degree.Student,
                Veteran = true,
                Account = new BankAccount(GetAccountList()[0], 4545),
                Specialty = 10000000,
                AccountNum = 4545,
                PhoneNumber = "05050505"
            };
            AddEmployee(tmp as Employee);
            tmp = new Employee()
            {
                Id = "124",
                LastName = "Cohen",
                FirstName = "Shalom",
                Birthdate = new DateTime(1990, 1, 1),
                Address = "My House 1",
                Education = Degree.Student,
                Veteran = true,
                Account = new BankAccount(GetAccountList()[0], 4545),
                Specialty = 10000000,
                AccountNum = 4545,
                PhoneNumber = "05050505"
            };
            AddEmployee(tmp as Employee);

            tmp = new Employer()
            {
                Id = "456",
                Address = "Someplace 7",
                CreationDate = new DateTime(2010, 2, 6),
                FirstName = "Issac",
                LastName = "Newton",
                PhoneNumber = "50505050",
                Specialty = SpecType.Gardening
            };
            AddEmployer(tmp as Employer);

            tmp = new Employer()
            {
                Id = "567",
                Address = "Someplace 7",
                CreationDate = new DateTime(2010, 2, 6),
                IsCompany = true,
                CompanyName = "Slaves & Co.",
                PhoneNumber = "50505050",
                Specialty = SpecType.Reanimation
            };
            AddEmployer(tmp as Employer);

            tmp = new Contract()
            {
                EmployeeId = "123",
                EmployerId = "456",
                StartDate = DateTime.Now,
                EndDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day),
                GrossSalary = 300,
                NetSalary = 200,
                TimeWorked = 160,
                WasInterviewed = true
            };
            AddContract(tmp as Contract);

            tmp = new Contract()
            {
                EmployeeId = "333",
                EmployerId = "456",
                StartDate = DateTime.Now,
                EndDate = new DateTime(DateTime.Now.Year + 3, DateTime.Now.Month, DateTime.Now.Day),
                GrossSalary = 30,
                NetSalary = 28,
                TimeWorked = 160,
                WasInterviewed = true
            };
            AddContract(tmp as Contract);

            tmp = new Contract()
            {
                EmployeeId = "124",
                EmployerId = "456",
                StartDate = DateTime.Now,
                EndDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day),
                GrossSalary = 35,
                NetSalary = 33,
                TimeWorked = 160,
                WasInterviewed = true
            };
            AddContract(tmp as Contract);
        }

        //-----ADD Functions-------------------------------------------------------------------------------

        /// <summary>
        /// checks if contract id is valid and enters it into datasource
        /// if id is 0, one will be assigned.
        /// exception: 
        /// -id is not 0 or 8 digits,
        /// -id already in system
        /// </summary>
        /// <param name="New">Contract to be added</param>
        public void AddContract(Contract New)
        {
            if (DataSource.Contracts == null) DataSource.Contracts = new List<Contract>();
            if (New.ContractId == 0)
                do
                {
                    New.ContractId = ContractNum++;
                } while (GetContractList().Exists(contract => contract.ContractId == New.ContractId));
            else
            {
                if (New.ContractId < 10000000 || New.ContractId > 99999999)
                    throw BE.Exceptions.Invalid_Id_Exception;
                Contract tmp = (DataSource.Contracts.Find(contract => contract.ContractId == New.ContractId));
                if (tmp != null)
                    throw BE.Exceptions.Id_Already_Registered_Exception;
            }
            New.Signed = true;
            DataSource.Contracts.Add(New);
        }

        /// <summary>
        /// checks if employee id is valid and enters it into datasource
        /// exceptions:
        /// -id smaller than 1 or not int
        /// -id already exists
        /// </summary>
        /// <param name="New">employee to be added</param>
        public void AddEmployee(Employee New)
        {
            int stam;
            if ((!int.TryParse(New.Id, out stam))|| (stam <=0))
                throw BE.Exceptions.Invalid_Id_Exception;

            else
            {
                if (DataSource.Employees == null) DataSource.Employees = new List<Employee>();
                Employee tmp = (DataSource.Employees.Find(employee => employee.Id == New.Id));
                if (tmp != null)
                    throw BE.Exceptions.Id_Already_Registered_Exception;
            }
            DataSource.Employees.Add(New);
        }

        /// <summary>
        /// checks if employer id is valid and enters it into datasource
        /// exceptions:
        /// -id smaller than 1 or not int
        /// -id already exists
        /// </summary>
        /// <param name="New">employer to be added</param>
        public void AddEmployer(Employer New)
        {
            if (DataSource.Employers == null) DataSource.Employers = new List<Employer>();
            int stam;
            if ((!int.TryParse(New.Id, out stam))|| (stam <=0))
                throw BE.Exceptions.Invalid_Id_Exception;

            else
            {
                Employer tmp = (DataSource.Employers.Find(employer => employer.Id == New.Id));
                if (tmp != null)
                    throw BE.Exceptions.Id_Already_Registered_Exception;
            }
            DataSource.Employers.Add(New);
        }

        /// <summary>
        /// checks if specialization id is valid and enters it into datasource
        /// if id is 0, one will be assigned.
        /// exceptions:
        /// -id is not 0 or 8 digits
        /// -id already exists
        /// </summary>
        /// <param name="New">specialization to be added</param>
        public void AddSpecialty(Specialization New)
        {
            if (DataSource.Specializations == null) DataSource.Specializations = new List<Specialization>();
            if (New.Id == 0)
                do
                {
                    New.Id = Specnum++;
                } while (GetSpecializationList().Exists(spec => spec.Id == New.Id));
            else
            {
                if (New.Id < 10000000 || New.Id > 99999999)
                    throw BE.Exceptions.Invalid_Id_Exception;
                Specialization tmp = (DataSource.Specializations.Find(specialization => specialization.Id == New.Id));
                if (tmp != null)
                    throw BE.Exceptions.Id_Already_Registered_Exception;
            }
            DataSource.Specializations.Add(New);
        }

        //-----EDIT Functions-------------------------------------------------------------------------------

        /// <summary>
        /// replaces existing data with new data.
        /// exceptions:
        /// -id is not 8 digits
        /// -id isn't in database
        /// </summary>
        /// <param name="currentId">id of contract to be edited</param>
        /// <param name="New">contract containing new data</param>
        public void EditContract(int currentId, Contract New)
        {
            if (currentId < 10000000 || currentId > 99999999)
                throw BE.Exceptions.Invalid_Id_Exception;
            int tmp = DataSource.Contracts.FindIndex(contract => contract.ContractId == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;
            New.ContractId = currentId;
            DataSource.Contracts[tmp] = New;
        }

        /// <summary>
        /// replaces existing data with new data.
        /// exceptions:
        /// -id smaller than 1 or not int
        /// -id not in database
        /// </summary>
        /// <param name="currentId">id of employee to be edited</param>
        /// <param name="New">employee containing new data</param>
        public void EditEmployee(string currentId, Employee New)
        {
            int stam;
            if ((!int.TryParse(New.Id, out stam))|| (stam <=0))
                throw BE.Exceptions.Invalid_Id_Exception;;

            int tmp = DataSource.Employees.FindIndex(employee => employee.Id == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;
            New.Id = currentId;
            DataSource.Employees[tmp] = New;
        }

        /// <summary>
        /// replaces existing data with new data.
        /// exceptions:
        /// -id smaller than 1 or not int
        /// -id isn't in database
        /// </summary>
        /// <param name="currentId">id of employer to be edited</param>
        /// <param name="New">employer containing new data</param>
        public void EditEmployer(string currentId, Employer New)
        {
            int stam;
            if ((!int.TryParse(New.Id, out stam))|| (stam <=0))
                throw BE.Exceptions.Invalid_Id_Exception;;

            int tmp = DataSource.Employers.FindIndex(employer => employer.Id == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;
            New.Id = currentId;
            DataSource.Employers[tmp] = New;
        }

        /// <summary>
        /// replaces existing data with new data
        /// </summary>
        /// <param name="currentId">id of specialty to be edited</param>
        /// <param name="New">specialty containing new data</param>
        public void EditSpecialty(int currentId, Specialization New)
        {
            if (currentId < 10000000 || currentId > 99999999)
                throw BE.Exceptions.Invalid_Id_Exception;
            int tmp = DataSource.Specializations.FindIndex(specialization => specialization.Id == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;
            New.Id = currentId;
            DataSource.Specializations[tmp] = New;
        }

        //------GET Functions-------------------------------------------------------------------------------------

        public List<BankAccount> GetAccountList()
        {
            List<BankAccount> tempList = new List<BankAccount>();
            BankAccount bank = new BankAccount();
            bank.AccountNum = -1;
            bank.BankNum = 1;
            bank.BankName = "Leumi";
            bank.City = "Jerusalem";
            bank.BranchNum = 752;
            bank.Address = "Shmuel Hanavi 39";
            tempList.Add(bank);

            bank = new BankAccount();
            bank.AccountNum = -1;
            bank.BankNum = 2;
            bank.BankName = "Yahav";
            bank.City = "Jerusalem";
            bank.BranchNum = 741;
            bank.Address = "Hillel 7";
            tempList.Add(bank);

            bank = new BankAccount();
            bank.AccountNum = -1;
            bank.BankNum = 3;
            bank.BankName = "Tfachot";
            bank.City = "Jerusalem";
            bank.BranchNum = 813;
            bank.Address = "Hagidem 17";
            tempList.Add(bank);

            bank = new BankAccount();
            bank.AccountNum = -1;
            bank.BankNum = 4;
            bank.BankName = "NullMoney";
            bank.City = "Tel-Aviv";
            bank.BranchNum = 1;
            bank.Address = "Tshirnochovski 6";
            tempList.Add(bank);

            bank = new BankAccount();
            bank.AccountNum = -1;
            bank.BankNum = 5;
            bank.BankName = "Discount";
            bank.City = "Jerusalem";
            bank.BranchNum = 993;
            bank.Address = "Yafo 5";
            tempList.Add(bank);

            return tempList;
        }

        public List<Contract> GetContractList()
        {
            //return DataSource.Contracts;
            return (from contract in DataSource.Contracts
                    select (Contract)contract.Clone()).ToList();
        }

        public List<Employee> GetEmployeeList()
        {
            // return DataSource.Employees;
            return (from employee in DataSource.Employees
                    select (Employee)employee.Clone()).ToList();
        }

        public List<Employer> GetEmployerList()
        {
            //return DataSource.Employers;
            return (from employer in DataSource.Employers
                    select (Employer)employer.Clone()).ToList();
        }

        public List<Specialization> GetSpecializationList()
        {
            //return DataSource.Specializations;
            return (from spec in DataSource.Specializations
                    select (Specialization)spec.Clone()).ToList();
        }

        //-----REMOVE Functions--------------------------------------------------------------------------------

        /// <summary>
        /// Remove contract that has selected id
        /// </summary>
        /// <param name="Id">id of contract to be removed</param>
        public void RemoveContract(int Id)
        {
            int tmp = DataSource.Contracts.RemoveAll(contract => contract.ContractId == Id);
            if (tmp == 0)
                throw BE.Exceptions.Id_Not_Found_Exception;
        }

        /// <summary>
        /// Remove employee that has selected id
        /// </summary>
        /// <param name="Id">id of employee to be removed</param>
        public void RemoveEmployee(string Id)
        {
            int tmp = DataSource.Employees.RemoveAll(employee => employee.Id == Id);//so we can use predicate

            if (tmp == 0)
                throw BE.Exceptions.Id_Not_Found_Exception;
            
        }

        /// <summary>
        /// Remove employer that has selected id
        /// </summary>
        /// <param name="Id">id of employer to be removed</param>
        public void RemoveEmployer(string Id)
        {
            int tmp = DataSource.Employers.RemoveAll(employer => employer.Id == Id);
            if (tmp == 0)
                throw BE.Exceptions.Id_Not_Found_Exception;
            
        }

        /// <summary>
        /// Remove specialty that has selected id
        /// </summary>
        /// <param name="Id">id of specialty to be removed</param>
        public void RemoveSpecialty(int Id)
        {
            int tmp = DataSource.Specializations.RemoveAll(specialty => specialty.Id == Id);
            if (tmp == 0)
                throw BE.Exceptions.Id_Not_Found_Exception;

            



        }




    }
}
