using System;
using System.Collections.Generic;
using System.Text;
using BankSystem.Interfaces;

namespace BankSystem.Classes.BankOperations
{
    public class Replenishment : IAccountOperation
    {
        public List<Client> Bank;
        public BankSystem.BankAccount Account;
        public double Sum;
        public double Comission;

        public Replenishment(BankSystem.BankAccount account, double sum, List<Client> clients, double comission)
        {
            Account = account;
            Sum = sum;
            Bank = clients;
            Comission = comission;
            Operation();
        }
        public Client IdentefyClient(BankSystem.BankAccount account)
        {
            foreach (Client client in Bank)
            {
                foreach (BankSystem.BankAccount bankAccount in client.Accounts)
                {
                    if (bankAccount == account)
                    {
                        return client;
                    }
                }
            }
            return null;
        }

        public void Operation()
        {
            var client = IdentefyClient(Account);
            Account.AccountStatus += Sum;
            Transaction transaction1 = new Transaction(Sum, Account, Transaction.TrasactionType.Replenishment);
            AddTransaction(client, transaction1);
        }
        public Transaction AddTransaction(Client client, Transaction transaction)
        {
            client.History.Add(transaction);
            return transaction;
        }

    }
}
