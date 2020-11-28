using System;
using System.Collections.Generic;
using BankSystem.Interfaces;

namespace BankSystem.Classes
{
    public class TransferOperation : IAccountOperation
    {
        public List<Client> Bank;
        public List<Client> Bank2;
        public BankAccount Client1;
        public BankAccount Client2;
        public double Sum;

        public TransferOperation(BankAccount client1, BankAccount client2, double sum, List<Client> clients, List<Client> clients2)
        {
            Client1 = client1;
            Client2 = client2;
            Sum = sum;
            Bank = clients;
            Bank2 = clients2;
            Operation();
        }
        public Client IdentefyClient(BankAccount account)
        {
           
            foreach (Client client in Bank)
            {
                foreach (BankAccount bankAccount in client.Accounts)
                {
                    if (bankAccount == account)
                    {
                        return client;
                    }
                }
            } 
            if (Bank != Bank2 && Bank2 != null)
            {
                foreach (Client client in Bank2)
                {
                    foreach (BankAccount bankAccount in client.Accounts)
                    {
                        if (bankAccount == account)
                        {
                            return client;
                        }
                    }
                }
            }
            return null;
        }


        public void Operation()
        {

            var client1 = IdentefyClient(Client1);
            var client2 = IdentefyClient(Client2);

            if (Client1.AccountStatus > Sum && Client1 != Client2)
            {
                Client1.AccountStatus -= Sum;

                Transaction transaction1 = new Transaction(-Sum, Client1);
                AddTransaction(client1, transaction1);

                Client2.AccountStatus += Sum;

                Transaction transaction2 = new Transaction(Sum, Client2);
                AddTransaction(client2, transaction2);
            }
            else
            {
                Console.WriteLine("Недостаточно средств для перевода");
            }
        }

        public Transaction AddTransaction(Client client, Transaction transaction)
        {
            client.History.Add(transaction);
            return transaction;
        }

    }
    public class Replenishment : IAccountOperation
    {
        public List<Client> Bank;
        public BankAccount Account;
        public double Sum;

        public Replenishment(BankAccount account, double sum, List<Client> clients)
        {
            Account = account;
            Sum = sum;
            Bank = clients;
            Operation();
        }
        public Client IdentefyClient(BankAccount account)
        {
            foreach (Client client in Bank)
            {
                foreach (BankAccount bankAccount in client.Accounts)
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
            Transaction transaction1 = new Transaction(Sum, Account);
            AddTransaction(client, transaction1);
        }
        public Transaction AddTransaction(Client client, Transaction transaction)
        {
            client.History.Add(transaction);
            return transaction;
        }

    }
    public class CashWithdrawal : IAccountOperation
    {
        public List<Client> Bank;
        public BankAccount Account;
        public double Sum;

        public CashWithdrawal(BankAccount account, double sum, List<Client> clients)
        {
            Account = account;
            Sum = sum;
            Bank = clients;
            Operation();
        }
        public Client IdentefyClient(BankAccount account)
        {
            foreach (Client client in Bank)
            {
                foreach (BankAccount bankAccount in client.Accounts)
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
            Transaction transaction1 = new Transaction(-Sum, Account, Transaction.TrasactionType.CashWithdrawal);
            AddTransaction(client, transaction1);
        }
        public Transaction AddTransaction(Client client, Transaction transaction)
        {
            client.History.Add(transaction);
            return transaction;
        }
    }
}
