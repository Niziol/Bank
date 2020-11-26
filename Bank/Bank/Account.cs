using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    abstract class Account
    {
        public int Id;
        public string AccountNumber;
        public decimal Balance;
        public string FirstName;
        public string LastName;
        public long Pesel;

        public Account(int id, string firstName, string lastName, long pesel)
        {
            Id = id;
            AccountNumber = generateAccountNumber(Id);
            Balance = 0.0M;
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
        }

        public abstract string TypeName();

        public string GetFullName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        public string GetBalance()
        {
            return string.Format("{0} zł", Balance);
        }

        private string generateAccountNumber(int id)
        {
            return string.Format("94{0:D10}", id);
        }
    }
}
