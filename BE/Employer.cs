using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employer : ICloneable
    {
        public string Id { get; set; }
        public bool IsCompany { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public SpecType Specialty { get; set; }




        public override string ToString()
        {

            if (!IsCompany) return "Number: "+ Id + "\n  " + FirstName + " " + LastName;
            else return "ID: " + Id + "\n  " + "\"" + CompanyName + "\"";

        }

        public object Clone()
        {

            return MemberwiseClone();
            //Employer old = new Employer();

            //old.Id            = Id;
            //old.IsCompany     = IsCompany;
            //old.CompanyName   = CompanyName;
            //old.LastName      = LastName;
            //old.FirstName         = FirstName;
            //old.CreationDate  = CreationDate;
            //old.PhoneNumber    = PhoneNumber;
            //old.Address       = Address;
            //old.Specialty     = Specialty;
            //old.NumJobs       = NumJobs;

            //return old;
        }

        public override bool Equals(object obj)
        {
            if (obj is Employer)
            {
                Employer check = (Employer)obj;
                return Id == check.Id &&
                        IsCompany == check.IsCompany &&
                        CompanyName == check.CompanyName &&
                        LastName == check.LastName &&
                        FirstName == check.FirstName &&
                        CreationDate == check.CreationDate &&
                        PhoneNumber == check.PhoneNumber &&
                        Address == check.Address &&
                        Specialty == check.Specialty;

            }

            else return false;
        }
    }
}
