using System;
using System.Collections.Generic;
using System.Text;
using BankSystem.Classes.BankAccount;
using BankSystem.Interfaces;

namespace BankSystem.Classes.BankOperations
{
    public class CashWithdrawal : IAccountOperation
    {
        public List<Client> Bank;
        public BankSystem.BankAccount Account;
        public double Sum;
        public double Comission;
        public double LimitForDoubfulperson;

        public CashWithdrawal(BankSystem.BankAccount account, double sum, List<Client> clients, double comission, double limitForDoubfulperson)
        {
            Account = account;
            Sum = sum;
            Bank = clients;
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

            if (clientAccount.AccountStatus < sum && clientAccount is CreditAccount)
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
}
