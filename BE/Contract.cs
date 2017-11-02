using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract : ICloneable
    {


        public int ContractId { get; set; }
        public string EmployerId { get; set; }
        public string EmployeeId { get; set; }
        public bool WasInterviewed { get; set; }
        public bool Signed { get; set; }
        public double GrossSalary { get; set; }
        public double NetSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TimeWorked { get; set; }



        public override string ToString()
        {
            return ContractId +
                "\n  Employer ID: " + EmployerId +
                "\n  Employee ID: " + EmployeeId;
        }

        public object Clone()
        {
            return MemberwiseClone();
            //Contract old = new Contract();

            //old.ContractId     = ContractId;
            //old.EmployerId    = EmployerId;
            //old.EmployeeId    = EmployeeId;
            //old.WasInterviewed     = WasInterviewed;
            //old.Signed            = Signed;
            //old.GrossSalary    = GrossSalary;
            //old.NetSalary         = NetSalary;
            //old.StartDate      = StartDate;
            //old.EndDate       = EndDate;
            //old.TimeWorked        = TimeWorked;

            //return old;
        }

        public override bool Equals(object obj)
        {
            if (obj is Contract)
            {
                Contract check = (Contract)obj;

                return ContractId == check.ContractId &&
                        EmployerId == check.EmployerId &&
                        EmployeeId == check.EmployeeId &&
                        WasInterviewed == check.WasInterviewed &&
                        Signed == check.Signed &&
                        GrossSalary == check.GrossSalary &&
                        NetSalary == check.NetSalary &&
                        StartDate == check.StartDate &&
                        EndDate == check.EndDate &&
                        TimeWorked == check.TimeWorked;
            }
            else return false;
        }
    }
}
