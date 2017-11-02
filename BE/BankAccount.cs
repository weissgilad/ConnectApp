using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct BankAccount
    {

        /// <summary>
        /// constructor to create new account with bank details
        /// </summary>
        /// <param name="bank"></param>
        /// <param name="AccountNum">account number for new account</param>
        public BankAccount(BankAccount bank, int AccountNum)
        {
            Address = bank.Address;
            BankName = bank.BankName;
            BankNum = bank.BankNum;
            BranchNum = bank.BranchNum;
            City = bank.City;
            this.AccountNum = AccountNum;
        }
        public int BankNum { get; set; }
        public string BankName { get; set; }
        public int BranchNum { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int AccountNum { get; set; }

        public override string ToString()
        {
            return BankName;
        }
        public override bool Equals(object obj)
        {
            if (obj is BankAccount)
            {
                BankAccount check = (BankAccount)obj;
                return BankNum == check.BankNum &&
                       BankName == check.BankName &&
                       BranchNum == check.BranchNum &&
                       Address == check.Address &&
                       City == check.City &&
                       AccountNum == check.AccountNum;
            }
            else return false;
        }
    }
}
