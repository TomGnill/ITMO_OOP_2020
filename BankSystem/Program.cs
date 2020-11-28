

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
            PassportData newData = new PassportData(2000, 11111);
            Adress newAdress = new Adress("КУКУЕВО", 12, "ХУЕВО");

            Person newPerson = new Person("Andrey" , "Loskutov", newAdress, newData);

            Person newPerson1 = new Person("Kristina", "Motherload");

            List<(double,double)> terms = new List<(double, double)>();
            terms.Add((110000, 0.1));
            terms.Add((210000, 0.2));
            terms.Add((410000, 0.3));


            Bank alfaBank = new Bank(terms,0.2,0.4,20000);
            Client newClient =  alfaBank.CreateClient(newPerson);

            Client newClient1 = alfaBank.CreateClient(newPerson1);
           // IAddAccount account =  alfaBank.AddDebitAccount(newClient, DateTime.Now);
            DebitAccount account = new DebitAccount(0.4,DateTime.Now);
            DebitAccount account2 = new DebitAccount(0.4, DateTime.Now);
            alfaBank.OpenAccount(newClient, account);
            alfaBank.OpenAccount(newClient1, account2);
            
            IAbstractBank abstractBank = alfaBank;
            abstractBank.Replenishment(account, 1000);

            double Status1 =  newClient.Accounts.ElementAt(0).AccountStatus;
            double Status3 = newClient1.Accounts.ElementAt(0).AccountStatus;

            Bank sberBank = new Bank(terms,0.2,0.4,20000);
            IAbstractBank abstractBanknew = sberBank;


            Client absrtactClient =  abstractBanknew.CreateClient(newPerson1); 
            abstractBanknew.AddDebitAccount(absrtactClient, DateTime.Now);


            BankAccount newAccount =  absrtactClient.Accounts.ElementAt(0);
            abstractBanknew.Replenishment(newAccount, 2000);
            abstractBanknew.Transfer(newAccount, account, 100, alfaBank);

            Console.WriteLine(absrtactClient.Accounts.ElementAt(0).AccountStatus);
            Console.WriteLine(newClient.Accounts.ElementAt(0).AccountStatus);
        }
    }
}
