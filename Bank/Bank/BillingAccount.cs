using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class BillingAccount : Account
    {
        public BillingAccount(int id, string accountNumber, decimal balance, string firstName, string lastName, long pesel)
            : base(id, firstName, lastName, pesel)
        {
        }

        public override string TypeName()
        {
            return "ROZLICZENIOWE";
        }
    }
}
