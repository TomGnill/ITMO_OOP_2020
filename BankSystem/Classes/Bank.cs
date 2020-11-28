using System;
using System.Collections.Generic;
using BankSystem.Classes;
using BankSystem.Interfaces;

namespace BankSystem
{
    public class Bank : IAbstractBank
    {
        public List<Client> Clients;
        //Terms:
        public List<(double,double)> procentOfDepozit; // начальная сумма : процент
        public double ResidueProcent;
        public double Commission;
        public double CreditLimit;
        public double limitForDoubfulPerson;


        public Bank(List<(double,double)> depositPrercent, double commission, double residueProcent, double creditLimit, double limitForDoubfulPerson )
        {
            Clients = new List<Client>();
            procentOfDepozit = depositPrercent;
            Commission = commission;
            ResidueProcent = residueProcent;
            CreditLimit = creditLimit;
            this.limitForDoubfulPerson = limitForDoubfulPerson;
        }

        public IAccountOperation Transfer(BankAccount account1, BankAccount account2, double cash, Bank secBank)
        {
            return new TransferOperation(account1,account2, cash , Clients, secBank.Clients, Commission, limitForDoubfulPerson);
        }

        public IAccountOperation Replenishment(BankAccount account, double cash)
        {
            return new Replenishment(account, cash, Clients , Commission);
        }

        public IAccountOperation CashWithdrawal(BankAccount account, double cash)
        {
            return new CashWithdrawal(account,cash, Clients, Commission , limitForDoubfulPerson);
        }

        public IAccountOperation ReturnMoney(Client client, int operationID)
        {
            return new CancelOperation(client, operationID);
        }

        public IAddAccount AddDepositAccount(Client client, double StartSum, DateTime startTime, DateTime endTime)
        {
            Deposit newDeposit =  new Deposit(StartSum,startTime,endTime,procentOfDepozit);
            OpenAccount(client, newDeposit);
            return newDeposit;
        }

        public IAddAccount AddDebitAccount(Client client, DateTime startTime)
        {
            DebitAccount newAccount = new DebitAccount(ResidueProcent, startTime);
            OpenAccount(client, newAccount);
            return newAccount;
        }

        public IAddAccount AddCreditAccount(Client client,  DateTime startTime)
        {
            CreditAccount newAccount = new CreditAccount(Commission, startTime, CreditLimit);
            OpenAccount(client, newAccount);
            return newAccount;
        }

        public double RefreshInfoAboutAccount(BankAccount account, DateTime time)
        {
            account.ReduceSum(time);
            return account.AccountStatus;
        }
        public void OpenAccount(Client client, BankAccount account)
        {
            foreach (Client user in Clients)
            {
                if (user == client)
                {
                    user.Accounts.Add(account);
                }
            }
        }

        public Client CreateClient(Person person)
        {
            List<BankAccount> nullList = new List<BankAccount>();
            Client newClient = new Client(person, nullList);
            Clients.Add(newClient);
            return newClient;
        }

        public void AddClientInfo(Client client, Adress addAdress, PassportData addPassportData)
        {
            if (client.Person.Loyalty == PersonLoyalty.Doubtful)
            {
                client.Person.PersonAdress = addAdress;
                client.Person.PersonData = addPassportData;
                client.Person.Loyalty = PersonLoyalty.Verified;
            }
            else
            {
                Console.WriteLine("Клиент уже заполнил все необходимые данные!");
            }
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
        }

        public void DeleteClient(Client client)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i] == client)
                {
                    Clients.RemoveAt(i);
                }
            }
        }

        public void BlockClient(Client client)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i] == client && Clients[i].status == ClientStatus.Active)
                {
                    Clients[i].status = ClientStatus.Blocked;
                }
            }

        }

        public void UnlockClient(Client client)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i] == client && Clients[i].status == ClientStatus.Blocked)
                {
                    Clients[i].status = ClientStatus.Active;
                }
            }
        }

        public void CancelOperation(Client client, Transaction transaction)
        {
            var cancelAc = transaction.Account;
            double cancelSum = transaction.Sum;
            for (int i = 0; i < client.Accounts.Count; i++)
            {
                if (client.Accounts[i] == cancelAc)
                {
                    client.Accounts[i].AccountStatus += -1 * cancelSum;
                }
            }
        }
    }
}
