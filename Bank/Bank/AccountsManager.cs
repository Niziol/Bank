using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class AccountsManager
    {
        private IList<Account> _accounts;

        public AccountsManager()
        {
            _accounts = new List<Account>();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accounts;
        }

        private int generateId()
        {
            int id = 1;
            if (_accounts.Any())
            {
                id = _accounts.Max(x => x.Id) + 1;
            }
            return id;
        }

        public SavingsAccount CreateSavingsAccount(string firstName, string lastName, long pesel)
        {
            int id = generateId();
            SavingsAccount account = new SavingsAccount(id, firstName, lastName, pesel);
            _accounts.Add(account);
            return account;
        }

        public BillingAccount CreateBillingAccount(string firstName, string lastName, long pesel)
        {
            int id = generateId();
            BillingAccount account = new BillingAccount(id, firstName, lastName, pesel);
            _accounts.Add(account);
            return account;
        }
        
        public IEnumerable<Account> GetAllAccountsFor(string firstName, string lastName, long pesel)
        {
            return _accounts.Where(x => x.FirstName == firstName && x.LastName == lastName && x.Pesel == pesel);
        }

        public Account GetAccount(string accountNumber)
        {
            return _accounts.Single(x => x.AccountNumber == accountNumber);
        }

        public IEnumerable<string> ListOfCustomers()
        {
            return _accounts.Select(x => string.Format("Imię: {0} | Nazwisko: {1} | PESEL: {2}", x.FirstName, x.LastName, x.Pesel)).Distinct();
        }

        public void CloseMonth()
        {
            foreach(SavingsAccount account in _accounts.Where(x => x is SavingsAccount))
            {
                account.AddInterest(0.04M);
            }
            foreach(BillingAccount account in _accounts.Where(x => x is BillingAccount))
            {
                account.TakeCharge(5.0M);
            }
        }
    }
}
