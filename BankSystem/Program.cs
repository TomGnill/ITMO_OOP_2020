

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
            List<(double, double)> TermForSecondtBank = new List<(double, double)>();

            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            TermForSecondtBank.Add((10000, 0.3));
            TermForSecondtBank.Add((30000, 0.4));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.2, 10000, 3000);
            Bank sberBank = new Bank(TermForSecondtBank, 0.2, 0.1, 5000, 1000);

            IAbstractBank firstBank = alfaBank;
            IAbstractBank secondBank = sberBank;

            Adress newAdress = new Adress("Невского", 10, "Питер");
            Adress additionalAdress = new Adress("Невского", 11, "Питер");

            PassportData firstPersonData = new PassportData(1111, 22222);
            PassportData secondPersonData = new PassportData(1111, 00000);

            Person Andrey = new Person("Андрей", "Загудько", newAdress, firstPersonData);
            Person Vasya = new Person("Вася", "Дударь");

            Client clientAndrey = firstBank.CreateClient(Andrey);
            Client clientVasya = secondBank.CreateClient(Vasya);

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);
            secondBank.AddDebitAccount(clientVasya, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);
            BankAccount firstVasyaAccount = clientVasya.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 2000);
            secondBank.Replenishment(firstVasyaAccount, 100);

            firstBank.Transfer(firstAndreyAccount, firstVasyaAccount, 400, sberBank); // Переводим 400 рублей Васе, зная что его счёт принадлежит сбербанку.

            firstBank.ReturnMoney(clientAndrey, 1); //тк это следующая посе пополнения операция, берём первый элемент из истории Андрея

          



        }
    }
}
