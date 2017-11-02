using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public class Employee : ICloneable
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthdate { get; set; } = DateTime.Now;
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Degree Education { get; set; }
        public bool Veteran { get; set; }
        public BankAccount Account { get; set; }
        public int Specialty { get; set; }
        public int AccountNum { get; set; }



        public override string ToString()
        {
            return"ID: " + Id + "\n  " + FirstName + " " + LastName;
        }

        public object Clone()
        {
            Employee old = new Employee();

            old.Id = Id;
            old.LastName = LastName;
            old.FirstName = FirstName;
            old.Birthdate = Birthdate;
            old.PhoneNumber = PhoneNumber;
            old.Address = Address;
            old.Education = Education;
            old.Veteran = Veteran;
            old.Account = new BankAccount(Account, AccountNum);
            old.Specialty = Specialty;
            old.AccountNum = AccountNum;

            return old;
        }

        public override bool Equals(object obj)
        {
            if (obj is Employee)
            {
                Employee check = (Employee)obj;

                return  Id == check.Id &&
                        LastName == check.LastName &&
                        FirstName == check.FirstName &&
                        Birthdate == check.Birthdate &&
                        PhoneNumber == check.PhoneNumber &&
                        Address == check.Address &&
                        Education == check.Education &&
                        Veteran == check.Veteran &&
                        Account.Equals(new BankAccount(check.Account, check.AccountNum)) &&
                        Specialty == check.Specialty &&
                        AccountNum == check.AccountNum;
            }
            else return false;
        }
    }
}
