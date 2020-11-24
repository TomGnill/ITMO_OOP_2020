using System;
using System.Collections.Generic;
using System.Text;
using BankSystem.Classes;

namespace BankSystem
{
    public class Bank : IAbstractBank
    {
        public List<Client> Clients;
        
        public Bank()
        {
            Clients = new List<Client>();
        }

        public IAccountOperation Transfer(BankAccount account1, BankAccount account2, double cash, Bank bank)
        {
            return new TransferOperation(account1,account2, cash , bank.Clients);
        }

        public IAccountOperation Replenishment(BankAccount account, double cash, Bank bank)
        {
            return new Replenishment(account, cash, bank.Clients);
        }

        public IAccountOperation CashWithdrawal(BankAccount account, double cash, Bank bank)
        {
            return new CashWithdrawal(account,cash, bank.Clients);
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
