using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Net;
using System.Net.Mail;

namespace BL
{
    /// <summary>
    /// buisiness layer, implementing the IBL interface
    /// </summary>
    public sealed class BL_imp : IBL
    {
        private const double BASE_TAX = 15;
        private const double TAX_REDUCE_EMPLOYEE = 0.05;
        private const double TAX_REDUCE_EMPLOYER = 0.05;

        Idal dal = FactoryDal.GetDal();



        internal BL_imp() { }


        /// <summary>
        /// check for internet connectivity
        /// </summary>
        /// <returns>true if internet is available</returns>
        private static bool CheckForInternetConnection()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    using (System.IO.Stream stream = client.OpenRead("http://www.google.com")) { return true; }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// send to employer and employee contract details
        /// </summary>
        /// <param name="Sender">name to show in email</param>
        /// <param name="JustSigned">contract for details</param>
        /// <param name="AddrEmper">employer email address</param>
        /// <param name="AddrEmpee">employee email address</param>
        /// <exception cref="NoIInternet_Exception"></exception>
        /// <exception cref="Could_Not_Send_Email_Exception"></exception>
        public void mail(string Sender, Contract JustSigned, string AddrEmper, string AddrEmpee)
        {
            Employee empee = GetEmployee(JustSigned.EmployeeId);
            Employer emper = GetEmployer(JustSigned.EmployerId);
            string eeName = empee.FirstName + " " + empee.LastName;
            string erName;
            if (emper.IsCompany)
                erName = "\"" + emper.CompanyName + "\"";
            else
                erName = emper.FirstName + " " + emper.LastName;

            if (!CheckForInternetConnection()) throw new Exception("No internet");

            MailAddress fromAddress = new MailAddress("jctlev16@gmail.com", Sender);
            const string fromPassword = "pruhheywpf";
            const string subject = "Your contract details";
            string body = "A contract was signed on " + DateTime.Now.ToString("dd/MM/yyyy") + "\n"
                + "Between " + erName + " as employer, and " + eeName + " as employee.\n" +
                "Gross salary is " + JustSigned.GrossSalary + " per hour, " + JustSigned.TimeWorked + " hours per month.\n" +
                "Contract is relevant from " + JustSigned.StartDate.ToString("dd/MM/yyyy") + " to " + JustSigned.EndDate.ToString("dd/MM/yyyy") +
                "\nThanks for using our services,\nConnect Them All Inc.";

            System.Net.Mail.SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            if (AddrEmpee != "")
            {
                MailAddress toAddress = new MailAddress(AddrEmpee, eeName);
                System.Net.Mail.MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };
                smtp.Send(message);
            }

            if (AddrEmper != "")
            {
                MailAddress toAddress = new MailAddress(AddrEmper, erName);
                System.Net.Mail.MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };
                smtp.Send(message);
            }
        }
        //ADD Functions------------------------------------------------------------------------------------------

        /// <summary>
        /// checks if a contract is valid, then adds it to database.
        /// </summary>
        /// <param name="New">contract to be inserted to database</param>
        public void AddContract(Contract New)
        {
            ContractValid(New);
            New.NetSalary = CalculateNet(New.EmployerId, New.EmployeeId, New.GrossSalary);

            dal.AddContract(New);
        }


        public void AddEmployee(Employee New)
        {
            if (New.AccountNum < 1)
                throw new Exception("invalid bank account number!");

            New.Account = new BankAccount(New.Account, New.AccountNum);
            EmployeeValid(New);


            dal.AddEmployee(New);
        }

        /// <summary>
        /// checks if an employee is valid, then adds him to database.
        /// </summary>
        /// <param name="New">employer to be inserted to database</param>
        public void AddEmployer(Employer New)
        {
            EmployerValid(New);
            dal.AddEmployer(New);
        }

        /// <summary>
        /// checks if a employee is valid, then adds it to database.
        /// </summary>
        /// <param name="New">specialty to be inserted to database</param>
        public void AddSpecialty(Specialization New)
        {
            SpecialtyValid(New);
            dal.AddSpecialty(New);
        }



        //EDIT Functions--------------------------------------------------------------------------------------------


        /// <summary>
        /// check if contract is valid and apply edits
        /// </summary>
        /// <param name="currentId">number of contract to be edited</param>
        /// <param name="New">new contract details</param>
        public void EditContract(int currentId, Contract New)
        {
            ContractValid(New);
            New.NetSalary = CalculateNet(New.EmployerId, New.EmployeeId, New.GrossSalary);
            dal.EditContract(currentId, New);
        }

        /// <summary>
        /// Check if employee is valid and apply edits
        /// </summary>
        /// <param name="currentId">ID of employee to be edited</param>
        /// <param name="New">new employee details</param>
        public void EditEmployee(string currentId, Employee New)
        {
            if (!EmployeeExists(currentId))
                throw BE.Exceptions.Id_Not_Found_Exception;

            EmployeeValid(New);
            New.Account = new BankAccount(New.Account, New.AccountNum);
            dal.EditEmployee(currentId, New);
        }

        /// <summary>
        /// check if employer is valid and apply changes
        /// </summary>
        /// <param name="currentId">id of employer to be edited</param>
        /// <param name="New">new employer details</param>
        public void EditEmployer(string currentId, Employer New)
        {
            EmployerValid(New);
            dal.EditEmployer(currentId, New);
        }

        /// <summary>
        /// check if specialization is valid and apply changes
        /// </summary>
        /// <param name="currentId">number of specialization to be edited</param>
        /// <param name="New">new specialization details</param>
        public void EditSpecialty(int currentId, Specialization New)
        {
            SpecialtyValid(New);
            dal.EditSpecialty(currentId, New);
        }


        //GET Functions------------------------------------------------------------------------------------------
        public List<BankAccount> GetAccountList()
        {
            return dal.GetAccountList();
        }

        public List<Contract> GetContractList()
        {

            return dal.GetContractList();
        }

        public List<Employee> GetEmployeeList()
        {

            return dal.GetEmployeeList();
        }

        public List<Employer> GetEmployerList()
        {

            return dal.GetEmployerList();
        }

        public List<Specialization> GetSpecializationList()
        {

            return dal.GetSpecializationList();
        }

        /// <summary>
        /// use id to find employee
        /// </summary>
        /// <param name="Id">id of employee to find</param>
        /// <returns></returns>
        public Employee GetEmployee(string Id)
        {
            if (!EmployeeExists(Id))
                throw BE.Exceptions.Id_Not_Found_Exception;

            return (from employee in GetEmployeeList()
                    where employee.Id == Id
                    select employee).FirstOrDefault();

            //return GetEmployeeList().Find(employee => employee.Id == Id);
        }

        /// <summary>
        /// use id to find employer
        /// </summary>
        /// <param name="Id">id of employer to find</param>
        /// <returns></returns>
        public Employer GetEmployer(string Id)
        {
            if (!EmployerExists(Id))
                throw BE.Exceptions.Id_Not_Found_Exception;

            return (from employer in GetEmployerList()
                    where employer.Id == Id
                    select employer).FirstOrDefault();

            //return GetEmployerList().Find(employer => employer.Id == Id);
        }

        /// <summary>
        /// use number to find specialization
        /// </summary>
        /// <param name="Id">number of specialization to find</param>
        /// <returns></returns>
        public Specialization GetSpecialization(int Id)
        {
            if (!SpecializationExists(Id))
                throw BE.Exceptions.Id_Not_Found_Exception;

            return (from spec in GetSpecializationList()
                    where spec.Id == Id
                    select spec).FirstOrDefault();

            //return GetSpecializationList().Find(special => special.Id == Id);
        }

        /// <summary>
        /// use number to find contract
        /// </summary>
        /// <param name="Id">number of contract to find</param>
        /// <returns></returns>
        public Contract GetContract(int Id)
        {
            if (!ContractExists(Id))
                throw BE.Exceptions.Id_Not_Found_Exception;

            return (from contract in GetContractList()
                    where contract.ContractId == Id
                    select contract).FirstOrDefault();

            // return GetContractList().Find(c => c.ContractId == Id);
        }

        public List<Employer> GetPrivateEmployerList()
        {
            return GetEmployerList().FindAll(e => !e.IsCompany);
        }

        public List<Employer> GetCompanyList()
        {
            return GetEmployerList().FindAll(e => e.IsCompany);
        }

        //REMOVE Functions------------------------------------------------------------------------

        public void RemoveContract(int Id)
        {

            dal.RemoveContract(Id);
        }

        public void RemoveEmployee(string Id)
        {
            var TempList = from contract in GetContractList()
                           where contract.EmployeeId == Id
                           select contract;
            foreach (var item in TempList)
            {
                RemoveContract(item.ContractId);
            }
            dal.RemoveEmployee(Id);
        }

        public void RemoveEmployer(string Id)
        {
            var TempList = from contract in GetContractList()
                           where contract.EmployerId == Id
                           select contract;
            foreach (var item in TempList)
            {
                RemoveContract(item.ContractId);
            }
            dal.RemoveEmployer(Id);
        }

        public void RemoveSpecialty(int Id)
        {
            var tempList1 = (from employee in GetEmployeeList()
                             where employee.Specialty == Id
                             select employee).ToList();

            foreach (var dead in tempList1)
            {
                RemoveEmployee(dead.Id);
            }

            dal.RemoveSpecialty(Id);
        }

        //EXISTS Functions--------------------------------------------------------------------------
        /// <summary>
        /// check if exists
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>true if exists</returns>
        private bool EmployerExists(string ID)
        {
            return GetEmployerList().Exists(employer => employer.Id == ID);
        }

        /// <summary>
        /// check if exists
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>true if exists</returns>
        private bool EmployeeExists(string ID)
        {
            return GetEmployeeList().Exists(employee => employee.Id == ID);
        }

        /// <summary>
        /// check if exists
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>true if exists</returns>
        private bool ContractExists(int ID)
        {
            return GetContractList().Exists(contract => contract.ContractId == ID);
        }

        /// <summary>
        /// check if exists
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>true if exists</returns>
        private bool AccountExists(BankAccount bank)
        {
            return GetEmployeeList().Exists(b => b.AccountNum == bank.BankNum && b.Account.BankName == bank.BankName);
        }

        /// <summary>
        /// check if exists
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>true if exists</returns>
        private bool SpecializationExists(int ID)
        {
            return GetSpecializationList().Exists((special => special.Id == ID));
        }

        //VALID Functions-------------------------------------------------------------------------

        /// <summary>
        /// this function exists to throw exceptions for any kind of problem
        /// </summary>
        /// <exception cref="Company_Date_Error_Exception">
        /// if buisiness is less than a year old or future date is given
        /// </exception>
        /// <param name="New">employer to check</param>
        private void EmployerValid(Employer New)
        {
            if (DateTime.Now < New.CreationDate)
                throw new Exception("you're being overly optimistic/nlooks like you haven't been created yet");

            try
            {
                if (int.Parse(New.PhoneNumber) < 1)
                    throw new Exception();
            }
            catch
            {
                throw new Exception("invalid phone number");
            }

            if (New.IsCompany && DateTime.Now - New.CreationDate < new TimeSpan(365, 0, 0, 0))
                throw new Exception("cannot sign with a company less than a year old");
        }

        /// <summary>
        /// this function exists to throw exceptions for any kind of problem
        /// </summary>
        /// <exception cref="Candidate_Too_Young_Exception"></exception>
        /// <exception cref="Bank_Already_Exists_Exception"></exception>
        /// <exception cref="No_Such_Specialization_Exception"></exception>
        /// <exception cref="Negative_Number_Exception">account num or phone</exception>
        /// <param name="New">employee to check</param>
        private void EmployeeValid(Employee New)
        {
            if (DateTime.Now - New.Birthdate < new TimeSpan(365 * 18, 0, 0, 0))
                throw new Exception("Candidate must be 18 or older!");


            try
            {
                if (int.Parse(New.PhoneNumber) < 1)
                    throw new Exception();
            }
            catch
            {
                throw new Exception("invalid phone number");
            }

            if (AccountExists(New.Account))
                throw new Exception("someone else has that bank account");

            if (!SpecializationExists(New.Specialty))
                throw new Exception("employee specialization not in system!");
        }

        /// <summary>
        /// this function exists to throw exceptions for any kind of problem
        /// </summary>
        /// <exception cref="Employee_And_Employer_Identical_Exception"></exception>
        /// <exception cref="Contract_Date_Problem_Exception"></exception>
        /// <exception cref="employee/employer_Dont_Exist_Exception"></exception>
        /// <exception cref="Work_Time_Invalid_Exception"></exception>
        /// <exception cref="Salary_Invalid_Exception"></exception>
        /// <param name="New">contract to check</param>
        private void ContractValid(Contract New)
        {
            if (New.EmployeeId == New.EmployerId)
                throw new Exception("who are you trying to fool?\nemployee and employer can't be the same person!");

            //if (New.EndDate < DateTime.Now)
            //    throw new Exception("why bother making a deal then?\nthe end time happened already");

            if (New.StartDate > DateTime.Now)
                throw new Exception("start date can't be retroactive");

            if (New.EndDate < New.StartDate)
                throw new Exception("end date can't be before start date");

            if (!EmployerExists(New.EmployerId))
                throw new Exception("specified employer is not registered...");

            if (!EmployeeExists(New.EmployeeId))
                throw new Exception("specified employee is not registered...");

            if (New.TimeWorked > 650)
                throw new Exception("that's waaaay too many hours");
            if (New.TimeWorked < 1)
                throw new Exception("that's waaaay too little hours");

            Specialization special = GetSpecialization(GetEmployee(New.EmployeeId).Specialty);
            if (New.GrossSalary < special.MinimumSalary)
                throw new Exception("salary is too low");
            if (New.GrossSalary > special.MaximumSalary)
                throw new Exception("salary is too high");
        }

        /// <summary>
        /// this function exists to throw exceptions for any kind of problem
        /// </summary>
        /// <exception cref="minimum_More_Than_Maximum_Exception"></exception>
        /// <exception cref="Salary_Too_Low_Exception"></exception>
        /// <param name="New">specialization to check</param>
        private void SpecialtyValid(Specialization New)
        {
            if (New.MaximumSalary < New.MinimumSalary)
                throw new Exception("Minimum Salary Can't be more than maximum salary");
            if (New.MinimumSalary < 0 || New.MaximumSalary < 0)
                throw new Exception("Salary can't be negative");
            if (New.MinimumSalary == 0)
                throw new Exception("We dont do slave labor here, minimum salary can't be 0");

        }


        /// <summary>
        /// calculates net salary based on gross salary and various employer/ee discounts
        /// </summary>
        /// <param name="employer"></param>
        /// <param name="employee"></param>
        /// <param name="gross"></param>
        /// <exception cref="File_Error_Exception"></exception>
        /// <returns>calculated net income</returns>
        private double CalculateNet(string employerId, string employeeId, double gross)
        {
            var contracts = GetContractList();

            double tax = BASE_TAX;
            tax *= Math.Pow(1 - TAX_REDUCE_EMPLOYEE, contracts.Count(e => e.EmployeeId == employeeId));
            tax *= Math.Pow(1 - TAX_REDUCE_EMPLOYER, contracts.Count(e => e.EmployerId == employerId));
            tax /= 100;
            return gross * (1 - tax);
        }

        /// <summary>
        /// function to find all compatible contracts and return them in a list
        /// </summary>
        /// <param name="foo">predicate to check contract compatibility</param>
        /// <returns>list of compatible contracts</returns>
        public IEnumerable<Contract> AvailableContracts(Predicate<Contract> foo)
        {
            //return dal.GetContractList().FindAll(foo);

            return from contract in GetContractList()
                   where foo(contract)
                   select contract;

        }
        //Grouping---------------------------------------------------------

        /// <summary>
        /// linq group employees by salary. will seperate by 1000s
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="sort">will sort if true</param>
        /// <returns>grouping</returns>
        public IEnumerable<IGrouping<int, Employee>> GroupBySalary(bool sort)
        {
            return from employee in GetEmployeeList()
                   let sum = AvailableContracts(contract => contract.EmployeeId == employee.Id).Sum(c => c.NetSalary * c.TimeWorked / 1000)
                   orderby sort ? sum : 0
                   group employee by (int)sum * 1000;
        }


        /// <summary>
        /// linq group by age
        /// </summary>
        /// <param name="sort">will sort if true</param>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Employee>> GroupByAgeGroup(bool sort)
        {
            return from employee in GetEmployeeList()
                   let age = (DateTime.Now - employee.Birthdate).Days / 3650
                   orderby sort ? age : 0
                   group employee by (int)age * 10;
        }

        /// <summary>
        /// linq group contracts by employee specialization
        /// </summary>
        /// <param name="sort">will sort if true</param>
        /// <returns></returns>
        public IEnumerable<IGrouping<SpecType, Contract>> GroupBySpecializationArea(bool sort)
        {
            return from contract in GetContractList()
                   let specialty = GetSpecialization(GetEmployee(contract.EmployeeId).Specialty).Type
                   orderby sort ? specialty : 0
                   group contract by specialty;
        }




        public IEnumerable<IGrouping<int, int>> GroupProfitYearly(bool sort)
        {
            return from contract in GetContractList()
                   let profitList = YearlyContractProfit(contract)
                   from item in profitList
                   group item.profit by item.year into g
                   let o = g.Key
                   let sum = (int)g.Sum()
                   orderby sort ? o : 0
                   group sum by g.Key;
        }


        public IEnumerable<IGrouping<int, int>> GroupByYearlyProfit(bool sort)
        {


            return from contract in GetContractList()
                   let profitList = YearlyContractProfit(contract)
                   from item in profitList
                   group item.profit by item.year into g
                   let sum = (int)g.Sum() / 100 * 100
                   orderby sort ? sum : 0
                   group g.Key by sum;
        }



        private List<YearProfit> YearlyContractProfit(Contract contract)
        {
            DateTime time = new DateTime(contract.StartDate.Ticks);
            DateTime end = new DateTime(contract.EndDate.Ticks);

            double sum = 0;
            List<YearProfit> YearList = new List<YearProfit>();
            while (true)
            {
                if (time.Year < end.Year)
                {
                    sum = ((new DateTime(time.Year, 12, 31) - time).Days / 31) * (contract.GrossSalary - contract.NetSalary);
                    YearProfit tmp = new YearProfit();
                    tmp.year = time.Year;
                    tmp.profit = sum;
                    YearList.Add(tmp);
                    time = new DateTime(time.Year + 1, 1, 1);

                }
                else
                {
                    sum = ((end - time).Days / 31) * (contract.GrossSalary - contract.NetSalary);
                    YearProfit tmp = new YearProfit();
                    tmp.year = time.Year;
                    tmp.profit = sum;
                    YearList.Add(tmp);
                    break;
                }
            }
            return YearList;
        }

        public IEnumerable<IGrouping<SpecType, Employer>> GroupEmployerBySpecializationArea(bool sort)
        {
            return from emp in GetEmployerList()
                   orderby sort ? emp.Specialty : 0
                   group emp by emp.Specialty;
        }

        public IEnumerable<IGrouping<int, Employer>> GroupEmployerByNumContracts(bool sort)
        {
            var list = GetContractList();
            return from emp in GetEmployerList()
                   let NumJobs = list.Count(e => e.EmployerId == emp.Id)
                   orderby sort ? NumJobs : 0
                   group emp by NumJobs;
        }

        public IEnumerable<IGrouping<int, Specialization>> GroupSpecializationByAverageGrossWage(bool sort)
        {
            var list = GetSpecializationList();
            return from con in GetContractList()
                   group con by list.Find(spec => (GetEmployee(con.EmployeeId).Specialty) == spec.Id) into g
                   let ave = ((int)g.Average(c => c.GrossSalary)) / 10 * 10
                   orderby sort ? ave : 0
                   group g.Key by ave;



        }

        public IEnumerable<IGrouping<int, Specialization>> GroupSpecializationByNumPeople(bool sort)
        {
            var list = GetSpecializationList();
            return from emp in GetEmployeeList()
                   group emp by list.Find(spec => spec.Id == emp.Specialty) into g
                   let sum = g.Count()
                   orderby sort ? sum : 0
                   group g.Key by sum;
        }

        public IEnumerable<IGrouping<string, IGrouping<string, BankAccount>>> GroupBankByNameAndCity()
        {
            return from bank in GetAccountList()
                   orderby bank.BankName, bank.City
                   group bank by bank.BankName into g
                   from NewGroup2 in (from bank2 in g
                                      group bank2 by bank2.City)
                   group NewGroup2 by g.Key;
        }
    }
}
