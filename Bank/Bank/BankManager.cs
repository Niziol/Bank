using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class BankManager
    {
        private AccountsManager _accountsManager;
        private IPrinter _printer;

        public BankManager()
        {
            _accountsManager = new AccountsManager();
            _printer = new ConsolePrinter();
        }

        private void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Wybierz akcję:");
            Console.WriteLine("1 - Lista kont klienta");
            Console.WriteLine("2 - Dodaj konto rozliczeniowe");
            Console.WriteLine("3 - Dodaj konto oszczędnościowe");
            Console.WriteLine("4 - Wpłać pieniądze na konto");
            Console.WriteLine("5 - Wypłać pieniądze z konta");
            Console.WriteLine("6 - Lista klientów");
            Console.WriteLine("7 - Wszystkie konta");
            Console.WriteLine("8 - Zakończ miesiąc");
            Console.WriteLine("0 - Zakończ");
        }

        public void Run()
        {
            int action;
            do
            {
                PrintMainMenu();
                action = SelectedAction();

                switch (action)
                {
                    case 1:
                        ListOfAccounts();
                        break;
                    case 2:
                        AddBillingAccount();
                        break;
                    case 3:
                        AddSavingsAccount();
                        break;
                    case 4:
                        AddMoney();
                        break;
                    case 5:
                        TakeMoney();
                        break;
                    case 6:
                        ListOfClients();
                        break;
                    case 7:
                        ListOfAllAccounts();
                        break;
                    case 8:
                        CloseMonth();
                        break;
                    default:
                        Console.Write("Nieznane polecenie");
                        Console.ReadKey();
                        break;
                }
            } while (action != 0);
        }

        public int SelectedAction()
        {
            Console.Write("Akcja: ");
            string action = Console.ReadLine();
            if (string.IsNullOrEmpty(action))
            {
                return -1;
            }
            return int.Parse(action);
        }

        private void ListOfAccounts()
        {
            Console.Clear();
            CustomerData data = ReadCustomerData();
            Console.WriteLine();
            Console.WriteLine("Konta klienta {0} {1} {2}", data.FirstName, data.LastName, data.Pesel);

            foreach(Account account in _accountsManager.GetAllAccountsFor(data.FirstName, data.LastName, data.Pesel))
            {
                _printer.Print(account);
            }
            Console.ReadKey();
        }

        private CustomerData ReadCustomerData()
        {
            string firstName;
            string lastName;
            string pesel;
            Console.WriteLine("Podaj dane klienta:");
            Console.Write("Imię: ");
            firstName = Console.ReadLine();
            Console.Write("Nazwisko: ");
            lastName = Console.ReadLine();
            Console.Write("PESEL: ");
            pesel = Console.ReadLine();

            return new CustomerData(firstName, lastName, pesel);
        }

        private void AddBillingAccount()
        {
            Console.Clear();
            CustomerData customerData = ReadCustomerData();
            Account billingAccount = _accountsManager.CreateBillingAccount(customerData.FirstName, customerData.LastName, customerData.Pesel);

            Console.WriteLine("Utworzono konto rozliczeniowe:");
            _printer.Print(billingAccount);
            Console.ReadKey();
        }

        private void AddSavingsAccount()
        {
            Console.Clear();
            CustomerData customerData = ReadCustomerData();
            Account savingAccount = _accountsManager.CreateSavingsAccount(customerData.FirstName, customerData.LastName, customerData.Pesel);

            Console.WriteLine("Utworzono konto oszczędnościowe:");
            _printer.Print(savingAccount);
            Console.ReadKey();
        }

        private void AddMoney()
        {
            Console.Clear();
            string accountNumber;
            decimal value;

            Console.WriteLine("Wpłata pieniędzy");
            Console.WriteLine("Numer konta: ");
            accountNumber = Console.ReadLine();
            Console.WriteLine("Kwota: ");
            value = decimal.Parse(Console.ReadLine());
            _accountsManager.AddMoney(accountNumber, value);

            Account account = _accountsManager.GetAccount(accountNumber);
            _printer.Print(account);

            Console.ReadKey();
        }

        private void TakeMoney()
        {
            Console.Clear();
            string accountNumber;
            decimal value;

            Console.WriteLine("Wypłata pieniędzy");
            Console.WriteLine("Numer konta: ");
            accountNumber = Console.ReadLine();
            Console.WriteLine("Kwota: ");
            value = decimal.Parse(Console.ReadLine());
            _accountsManager.TakeMoney(accountNumber, value);

            Account account = _accountsManager.GetAccount(accountNumber);
            _printer.Print(account);
            Console.ReadKey();
        }

        private void ListOfClients()
        {
            Console.Clear();
            Console.WriteLine("Lista klientów:");
            foreach(string customer in _accountsManager.ListOfCustomers())
            {
                Console.WriteLine(customer);
            }
            Console.ReadKey();
        }

        private void ListOfAllAccounts()
        {
            Console.Clear();
            Console.WriteLine("Lista wszystkich kont:");
            foreach(Account account in _accountsManager.GetAllAccounts())
            {
                _printer.Print(account);
            }
            Console.ReadKey();
        }

        private void CloseMonth()
        {
            Console.Clear();
            _accountsManager.CloseMonth();
            Console.WriteLine("Miesiąc zamknięty");
            Console.ReadKey();
        }
    }

    class CustomerData
    {
        public string FirstName { get; }
        public string LastName { get; }
        public long Pesel { get; }

        public CustomerData(string firstName, string lastName, string pesel)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = long.Parse(pesel);
        }
    }
}
