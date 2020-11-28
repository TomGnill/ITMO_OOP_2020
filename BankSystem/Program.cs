

using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.Classes;
using BankSystem.Interfaces;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("Невского", 10, "Питер");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("Андрей", "Загудько", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddCreditAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 1000);

            firstBank.CashWithdrawal(firstAndreyAccount, 1000);

            firstBank.CashWithdrawal(firstAndreyAccount, 200);

            Console.WriteLine(firstAndreyAccount.AccountStatus);
        }
    }
}
