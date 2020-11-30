using System;
using System.Collections.Generic;
using BankSystem.Classes;
using BankSystem.Interfaces;

namespace BankSystem
{
   
    public interface IAbstractBank
    {

        public void AddClient(Client client);

        public void OpenAccount(Client client, BankAccount account);

        public void DeleteClient(Client client);

        public void BlockClient(Client client);

        public void UnlockClient(Client client);

        public Client CreateClient(Person person);

        public void AddClientInfo(Client client, Adress addAdress, PassportData addPassportData);

        public double RefreshInfoAboutAccount(BankAccount account, DateTime time);
        public IAccountOperation Transfer(BankAccount account1, BankAccount account2, double cash, Bank secondBank);

        public IAccountOperation Replenishment(BankAccount account,double cash);

        public IAccountOperation CashWithdrawal(BankAccount account, double cash);

        public IAccountOperation ReturnMoney(Client client, int operationID);

        public IAddAccount AddDepositAccount(Client client, double startSum, DateTime startTime, DateTime endTime);

        public IAddAccount AddDebitAccount(Client client, DateTime startTime);

        public IAddAccount AddCreditAccount(Client client,  DateTime startTime);

    }

}
