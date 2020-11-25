using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using BankSystem.Classes;
using BankSystem.Interfaces;

namespace BankSystem
{
    public  class Bank : IAbstractBank
    {
        public List<Client> Clients;
        //Terms:
        public List<(double,double)> procentOfDepozit; // начальная сумма : процент
        public double ResidueProcent;
        public double Commission;
        public double CreditLimit;


        public Bank(List<(double,double)> depositPrercent, double commission, double residueProcent, double creditLimit )
        {
            Clients = new List<Client>();
            procentOfDepozit = depositPrercent;
            Commission = commission;
            ResidueProcent = residueProcent;
            CreditLimit = creditLimit;
        }

        public IAccountOperation Transfer(BankAccount account1, BankAccount account2, double cash)
        {
            return new TransferOperation(account1,account2, cash , Clients);
        }

        public IAccountOperation Replenishment(BankAccount account, double cash)
        {
            return new Replenishment(account, cash, Clients);
        }

        public IAccountOperation CashWithdrawal(BankAccount account, double cash)
        {
            return new CashWithdrawal(account,cash, Clients);
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
    }
}
