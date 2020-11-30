using System;
using System.Collections.Generic;
using System.Text;
using BankSystem.Classes.BankAccount;
using BankSystem.Interfaces;

namespace BankSystem.Classes.BankOperations
{
    public class TransferOperation : IAccountOperation
    {
        public Bank bank;
        public List<Client> Bank;
        public List<Client> Bank2;
        public BankSystem.BankAccount Client1;
        public BankSystem.BankAccount Client2;
        public double Sum;
        public double Comission;
        public double LimitForDoubfulperson;

        public TransferOperation(BankSystem.BankAccount client1, BankSystem.BankAccount client2, double sum, List<Client> clients, List<Client> clients2, double comission, double limitForDoubfulperson)
        {
            Client1 = client1;
            Client2 = client2;
            Sum = sum;
            Bank = clients;
            Bank2 = clients2;
            Comission = comission;
            LimitForDoubfulperson = limitForDoubfulperson;
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
            if (Bank != Bank2 && Bank2 != null)
            {
                foreach (Client client in Bank2)
                {
                    foreach (BankSystem.BankAccount bankAccount in client.Accounts)
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


            if (CheckOperationTerms(client1, Sum, Client1))
            {

                if (Client1 is CreditAccount && Client1.AccountStatus <= 0)
                {
                    Sum += Sum * Comission;
                }

                if (Client1.AccountStatus >= Sum && Client1 != Client2)
                {
                    Client1.AccountStatus -= Sum;

                    Transaction transaction1 = new Transaction(-Sum, Client1, Client2);
                    AddTransaction(client1, transaction1);

                    Client2.AccountStatus += Sum;

                    Transaction transaction2 = new Transaction(Sum, Client1, Client2);
                    AddTransaction(client2, transaction2);
                }
                else
                {
                    Console.WriteLine("Недостаточно средств для перевода");
                }

            }
            else
            {
                Console.WriteLine("Клиент не удовлетворяет условиям");
            }
        }

        public Transaction AddTransaction(Client client, Transaction transaction)
        {
            client.History.Add(transaction);
            return transaction;
        }

        public bool CheckOperationTerms(Client client, double sum, BankSystem.BankAccount clientAccount)
        {
            if (client.Person.PersonAdress == null && client.Person.PersonData == null && sum > LimitForDoubfulperson)
            {
                return false;
            }

            if (client.status == ClientStatus.Blocked)
            {
                return false;
            }

            if (clientAccount.Status == AccountStatus.Sleep)
            {
                return false;
            }

            if (clientAccount.AccountStatus < 0 && clientAccount is CreditAccount)
            {
                Sum += Comission * sum;
                return true;
            }

            if (clientAccount.AccountStatus < 0)
            {
                return false;
            }

            return true;
        }

    }
}
