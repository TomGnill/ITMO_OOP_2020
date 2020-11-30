using System;
using System.Collections.Generic;
using BankSystem.Interfaces;

namespace BankSystem.Classes
{
    public class TransferOperation : IAccountOperation
    {
        public Bank bank;
        public List<Client> Bank;
        public List<Client> Bank2;
        public BankAccount Client1;
        public BankAccount Client2;
        public double Sum;
        public double Comission;
        public double LimitForDoubfulperson;

        public TransferOperation(BankAccount client1, BankAccount client2, double sum, List<Client> clients, List<Client> clients2, double comission, double limitForDoubfulperson)
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

        public bool CheckOperationTerms(Client client, double sum, BankAccount clientAccount)
        {
            if (client.Person.Loyalty == PersonLoyalty.Doubtful && sum > LimitForDoubfulperson)
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

            if (clientAccount.Status < 0 && clientAccount is CreditAccount)
            {
                Sum += Comission * sum;
                return true;
            }

            if (clientAccount.Status < 0)
            {
                return false;
            }

            return true;
        }

    }
    public class Replenishment : IAccountOperation
    {
        public List<Client> Bank;
        public BankAccount Account;
        public double Sum;
        public double Comission;

        public Replenishment(BankAccount account, double sum, List<Client> clients, double comission)
        {
            Account = account;
            Sum = sum;
            Bank = clients;
            Comission = comission;
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
            Transaction transaction1 = new Transaction(Sum, Account, Transaction.TrasactionType.Replenishment);
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
        public double Comission;
        public double LimitForDoubfulperson;

        public CashWithdrawal(BankAccount account, double sum, List<Client> clients, double comission, double limitForDoubfulperson)
        {
            Account = account;
            Sum = sum;
            Bank = clients;
            Comission = comission;
            LimitForDoubfulperson = limitForDoubfulperson;
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
            if (CheckOperationTerms(client, Sum, Account))
            {
                Account.AccountStatus -= Sum;
                Transaction transaction1 = new Transaction(-Sum, Account, Transaction.TrasactionType.CashWithdrawal);
                AddTransaction(client, transaction1);
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

        public bool CheckOperationTerms(Client client, double sum, BankAccount clientAccount)
        {
            if (client.Person.Loyalty == PersonLoyalty.Doubtful && sum > LimitForDoubfulperson)
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

            if (clientAccount.AccountStatus < sum && clientAccount  is  CreditAccount)
            {
                Sum += sum * Comission;
                return true;
            }

            if (clientAccount.AccountStatus < sum)
            {
                return false;
            }

            return true;
        }
    }

    class CancelOperation : IAccountOperation
    {
        public Client client;
        public int TransactionNumber;
        public CancelOperation(Client theClient, int transactionNumber)
        {
            client = theClient;
            TransactionNumber = transactionNumber;
            Operation();
        }

        public Transaction identefyTransaction(int transactionNumber)
        {
            for (int i=0;i<client.History.Count;i++)
            {
                if (i == transactionNumber)
                {
                     return client.History[i];
                }
            }

            return null;
        }
        public void Operation()
        {
            var transaction = identefyTransaction(TransactionNumber);
            if (transaction.Type != Transaction.TrasactionType.Transfer)
            {
                var cancelAc = transaction.Account;
                double cancelSum = transaction.Sum;
                cancelAc.AccountStatus += -1 * cancelSum;
            }
            else
            {
                var canelAcFrst = transaction.Account;
                var cancelAcSec = transaction.AccountSec;

                double cancelSum = transaction.Sum;

                canelAcFrst.AccountStatus += -1 * cancelSum;
                cancelAcSec.AccountStatus +=  cancelSum;
            }
        }
    }
}
