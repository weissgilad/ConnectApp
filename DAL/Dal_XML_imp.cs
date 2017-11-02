using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Threading;

namespace DAL
{
    class Dal_XML_imp : Idal
    {

        enum XML_Action
        {
            Load,
            Save
        }

        //Idal dal = new Dal_imp();//TEMPORARY!!

        private static int ContractNum;// = 10000000;
        private static int Specnum;// = 10000000;
        private static List<BankAccount> Banks = null;


        internal Dal_XML_imp() { }
        #region old xml
        //private List<T> readXml<T>(string path)
        //{
        //    if (!File.Exists(path))
        //    {
        //        FileStream tmpFs = new FileStream(path, FileMode.Create);
        //        XmlSerializer sere = new XmlSerializer(typeof(List<T>));
        //        sere.Serialize(tmpFs, new List<T>());
        //        tmpFs.Close();
        //    }

        //    FileStream fs = new FileStream(path, FileMode.Open);

        //    XmlSerializer dese = new XmlSerializer(typeof(List<T>));
        //    var tmp = ((IEnumerable<T>)dese.Deserialize(fs)).ToList();
        //    fs.Close();
        //    return tmp;
        //}

        //private void writeXml<T>(IEnumerable<T> ToWrite, string path)
        //{
        //    FileStream fs = new FileStream(path, FileMode.Create);

        //    XmlSerializer sere = new XmlSerializer(typeof(List<T>));
        //    sere.Serialize(fs, ToWrite);
        //    fs.Close();
        //}
        #endregion

        /// <summary>
        /// load or save xml
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="action"></param>
        private void SyncXmlSettings(XML_Action action)
        {
            XElement settings;
            try
            {
                switch (action)
                {
                    case XML_Action.Save:
                        {
                            settings = new XElement("Settings",
                                                    new XElement("Contract_number", ContractNum),
                                                    new XElement("Specialization_number", Specnum)
                                                    );
                            settings.Save(@"Settings.xml");
                            break;
                        }

                    case XML_Action.Load:
                        {
                            if (!File.Exists("Settings.xml"))
                            {
                                new FileStream("Settings.xml", FileMode.Create).Close();

                                new XElement("Settings",
                                                    new XElement("Contract_number", 10000000),
                                                    new XElement("Specialization_number", 10000000)
                                                    ).Save(@"Settings.xml");
                            }
                            settings = XElement.Load(@"Settings.xml");
                            ContractNum = Convert.ToInt32(settings.Element("Contract_number").Value);
                            Specnum = Convert.ToInt32(settings.Element("Specialization_number").Value);
                            break;
                        }
                }
            }
            catch
            {
                throw Exceptions.File_Error_Exception;
            }



        }

        /// <summary>
        /// load or save xml
        /// </summary>
        /// <exception cref="File_Error_Exception"></exception>
        /// <param name="action"></param>
        private List<T> SyncXML<T>(XML_Action action, string path, IEnumerable<T> ToWrite = null)
        {
            switch (action)
            {
                case XML_Action.Save:
                    if (ToWrite != null)
                    {
                        FileStream fs = new FileStream(path, FileMode.Create);
                        XmlSerializer sere = new XmlSerializer(typeof(List<T>));
                        sere.Serialize(fs, ToWrite);
                        fs.Close();
                    }
                    return null;

                case XML_Action.Load:
                    {
                        if (!File.Exists(path))
                        {
                            FileStream tmpFs = new FileStream(path, FileMode.Create);
                            XmlSerializer sere = new XmlSerializer(typeof(List<T>));
                            sere.Serialize(tmpFs, new List<T>());
                            tmpFs.Close();
                        }

                        FileStream fs = new FileStream(path, FileMode.Open);

                        XmlSerializer dese = new XmlSerializer(typeof(List<T>));
                        var tmp = ((IEnumerable<T>)dese.Deserialize(fs)).ToList();
                        fs.Close();
                        return tmp;
                    }
                default: return null;
            }
        }

        public void AddContract(Contract New)
        {
            var list = GetContractList();
            SyncXmlSettings(XML_Action.Load);
            if (New.ContractId == 0)
                do
                {
                    New.ContractId = ContractNum++;
                } while (list.Exists(contract => contract.ContractId == New.ContractId));
            else
            {
                if (New.ContractId < 10000000 || New.ContractId > 99999999)
                    throw Exceptions.Invalid_Id_Exception;
                Contract tmp = (list.Find(contract => contract.ContractId == New.ContractId));
                if (tmp != null)
                    throw Exceptions.Id_Already_Registered_Exception;
            }
            New.Signed = true;
            list.Add(New);
            SyncXmlSettings(XML_Action.Save);
            //writeXml<Contract>(list, "Contracts.xml");
            SyncXML<Contract>(XML_Action.Save, "Contracts.xml", list);
        }

        public void AddEmployee(Employee New)
        {
            var list = GetEmployeeList();
            int stam;

            if ((!int.TryParse(New.Id, out stam)) || (stam <= 0))
                throw BE.Exceptions.Invalid_Id_Exception; ;

            Employee tmp = (list.Find(employee => employee.Id == New.Id));
            if (tmp != null)
                throw BE.Exceptions.Id_Already_Registered_Exception;

            list.Add(New);
            //writeXml<Employee>(list, "Employees.xml");
            SyncXML<Employee>(XML_Action.Save, "Employees.xml", list);
        }

        public void AddEmployer(Employer New)
        {
            var list = GetEmployerList();


            int stam;
            if ((!int.TryParse(New.Id, out stam)) || (stam <= 0))
                throw BE.Exceptions.Invalid_Id_Exception; ;

            Employer tmp = (list.Find(employer => employer.Id == New.Id));
            if (tmp != null)
                throw BE.Exceptions.Id_Already_Registered_Exception;

            list.Add(New);
            //writeXml<Employer>(list, "Employers.xml");
            SyncXML<Employer>(XML_Action.Save, "Employers.xml", list);
        }

        public void AddSpecialty(Specialization New)
        {

            SyncXmlSettings(XML_Action.Load);
            var CheckList = GetSpecializationList(); //ONLY FOR CHECKING VALIDITY!! 

            if (New.Id == 0)
                do
                {
                    New.Id = Specnum++;
                } while (CheckList.Exists(spec => spec.Id == New.Id));
            else
            {
                if (New.Id < 10000000 || New.Id > 99999999)
                    throw BE.Exceptions.Invalid_Id_Exception;
                Specialization tmp = (CheckList.Find(specialization => specialization.Id == New.Id));
                if (tmp != null)
                    throw BE.Exceptions.Id_Already_Registered_Exception;
            }
            if (New.SpecialtyName == null || New.SpecialtyName == "")
                throw new Exception("Invalid specialty name");

            XElement AddSpec = new XElement("Specialization",
                                            new XElement("Id", New.Id),
                                            new XElement("Type", New.Type),
                                            new XElement("SpecialtyName", New.SpecialtyName),
                                            new XElement("MaximumSalary", New.MaximumSalary),
                                            new XElement("MinimumSalary", New.MinimumSalary));
            try
            {
                XElement SpecRoot = XElement.Load(@"Specializations.xml");
                SpecRoot.Add(AddSpec);
                SpecRoot.Save(@"Specializations.xml");
            }
            catch
            {
                throw Exceptions.File_Error_Exception;
            }
            finally
            {
                SyncXmlSettings(XML_Action.Save);
            }


        }

        public void EditContract(int currentId, Contract New)
        {
            var list = GetContractList();

            if (currentId < 10000000 || currentId > 99999999)
                throw BE.Exceptions.Invalid_Id_Exception;
            int tmp = list.FindIndex(contract => contract.ContractId == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;
            New.ContractId = currentId;
            list[tmp] = New;

            //writeXml<Contract>(list, "Contracts.xml");
            SyncXML<Contract>(XML_Action.Save, "Contracts.xml", list);
        }

        public void EditEmployee(string currentId, Employee New)
        {
            var list = GetEmployeeList();
            int stam;
            if ((!int.TryParse(New.Id, out stam)) || (stam <= 0))
                throw BE.Exceptions.Invalid_Id_Exception; ;

            int tmp = list.FindIndex(employee => employee.Id == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;
            New.Id = currentId;
            list[tmp] = New;

            //writeXml<Employee>(list, "Employees.xml");
            SyncXML<Employee>(XML_Action.Save, "Employees.xml", list);
        }

        public void EditEmployer(string currentId, Employer New)
        {
            var list = GetEmployerList();
            int stam;
            if ((!int.TryParse(New.Id, out stam)) || (stam <= 0))
                throw BE.Exceptions.Invalid_Id_Exception; ;

            int tmp = list.FindIndex(employer => employer.Id == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;
            New.Id = currentId;
            list[tmp] = New;

            //writeXml<Employer>(list, "Employers.xml");
            SyncXML<Employer>(XML_Action.Save, "Employers.xml", list);
        }

        public void EditSpecialty(int currentId, Specialization New)
        {
            if (currentId < 10000000 || currentId > 99999999)
                throw BE.Exceptions.Invalid_Id_Exception;
            int tmp = GetSpecializationList().FindIndex(specialization => specialization.Id == currentId);
            if (tmp == -1)
                throw BE.Exceptions.Id_Not_Found_Exception;

            New.Id = currentId;
            RemoveSpecialty(currentId);
            AddSpecialty(New);
        }

        public List<BankAccount> GetAccountList()
        {
            const string xmlLocalPath = @"Banks.xml";

            if (Banks != null)
                return Banks;
            else
            {
                downloadBankInfoXml(xmlLocalPath);
                XElement BankXml = XElement.Load(xmlLocalPath);

                Banks = new List<BankAccount>();


                foreach (var item in BankXml.Elements())
                {
                    Banks.Add(
                                  new BankAccount()
                                  {
                                      AccountNum = -1,
                                      BankNum = int.Parse(item.Element("Bank_Code").Value),
                                      BankName = item.Element("Bank_Name").Value.Replace('\n', ' ').Replace("בע\"מ", "").Trim(),
                                      BranchNum = int.Parse(item.Element("Branch_Code").Value),
                                      Address = item.Element("Branch_Address").Value.Replace('\n', ' ').Trim(),
                                      City = item.Element("City").Value.Replace('\n', ' ').Trim()
                                  }
                               );
                }
                return Banks;
            }


        }


        /// <summary>
        /// download bank info into file
        /// </summary>
        /// <exception cref="WebClient_Exception"></exception>
        /// <param name="xmlLocalPath"></param>
        private void downloadBankInfoXml(string xmlLocalPath)
        {

            const string xmlBankServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_dnld_he.xml";
            const string xmlMyServerPath = @"https://dl.dropboxusercontent.com/s/5owx1f2d3xlhwr9/snifim_dnld_he.xml?dl=0";

            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(xmlBankServerPath, xmlLocalPath);
            }
            catch
            {
                wc.DownloadFile(xmlMyServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }
        }

        public List<Contract> GetContractList()
        {
            try { return SyncXML<Contract>(XML_Action.Load, "Contracts.xml"); }
            catch { throw Exceptions.File_Error_Exception; }
        }

        public List<Employee> GetEmployeeList()
        {
            try { return SyncXML<Employee>(XML_Action.Load, "Employees.xml"); }
            catch { throw Exceptions.File_Error_Exception; }
        }

        public List<Employer> GetEmployerList()
        {
            try { return SyncXML<Employer>(XML_Action.Load, "Employers.xml"); }
            catch { throw Exceptions.File_Error_Exception; }
        }

        public List<Specialization> GetSpecializationList()
        {
            try
            {
                if (!File.Exists("Specializations.xml"))
                {
                    XDocument tmp = new XDocument();
                    tmp.Add(new XElement("ArrayOfSpecilaization"));
                    tmp.Save(@"Specializations.xml");

                }

                XElement SpecRoot = XElement.Load(@"Specializations.xml");

                return (from spec in SpecRoot.Elements()
                        select new Specialization()
                        {
                            Id = Convert.ToInt32(spec.Element("Id").Value),
                            Type = (SpecType)Enum.Parse(typeof(SpecType), spec.Element("Type").Value),
                            SpecialtyName = spec.Element("SpecialtyName").Value,
                            MaximumSalary = Convert.ToDouble(spec.Element("MaximumSalary").Value),
                            MinimumSalary = Convert.ToDouble(spec.Element("MinimumSalary").Value)
                        }).ToList();
            }
            catch { throw Exceptions.File_Error_Exception; }
        }

        public void RemoveContract(int Id)
        {
            var list = GetContractList();
            int tmp = list.RemoveAll(contract => contract.ContractId == Id);
            if (tmp == 0)
                throw BE.Exceptions.Id_Not_Found_Exception;


            //writeXml<Contract>(list, "Contracts.xml");
            SyncXML<Contract>(XML_Action.Save, "Contracts.xml", list);
        }

        public void RemoveEmployee(string Id)
        {
            var list = GetEmployeeList();
            int tmp = list.RemoveAll(employee => employee.Id == Id);//so we can use predicate

            if (tmp == 0)
                throw Exceptions.Id_Not_Found_Exception;


            //writeXml<Employee>(list, "Employees.xml");
            SyncXML<Employee>(XML_Action.Save, "Employees.xml", list);
        }

        public void RemoveEmployer(string Id)
        {
            var list = GetEmployerList();
            int tmp = list.RemoveAll(employer => employer.Id == Id);//so we can use predicate

            if (tmp == 0)
                throw BE.Exceptions.Id_Not_Found_Exception;


            //writeXml<Employer>(list, "Employers.xml");
            SyncXML<Employer>(XML_Action.Save, "Employers.xml", list);
        }

        public void RemoveSpecialty(int Id)
        {
            XElement SpecRoot;
            XElement XdeadSpec;
            try { SpecRoot = XElement.Load(@"Specializations.xml"); }
            catch { throw new Exception("File upload problem"); }

            try
            {
                XdeadSpec = (from spec in SpecRoot.Elements()
                             where Convert.ToInt32(spec.Element("Id").Value) == Id
                             select spec).FirstOrDefault();
                XdeadSpec.Remove();
                SpecRoot.Save(@"Specializations.xml");
            }
            catch
            {
                throw BE.Exceptions.Id_Not_Found_Exception;
            }


        }
    }
}
