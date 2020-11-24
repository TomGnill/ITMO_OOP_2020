﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Classes
{
   public class Client
   {
        public Person Person;
        public List<BankAccount> Accounts;
        public List<Transaction> History;
        public ClientStatus status;

        public Client(Person person, List<BankAccount> accounts)
        {
            Person = person;
            Accounts = accounts;
            status = ClientStatus.Active;
        }

        public BankAccount AddAccount(BankAccount newAccount)
        {
            Accounts.Add(newAccount);
            return newAccount;
        }
   }

  public enum ClientStatus 
  {
       Blocked,
       Active
  }
}
