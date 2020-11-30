using System.Collections.Generic;
namespace BankSystem.Classes
{
   public class Client
   {
        public BankSystem.Person Person;
        public List<BankSystem.BankAccount> Accounts;
        public List<Transaction> History;
        public ClientStatus status;

        public Client(BankSystem.Person person, List<BankSystem.BankAccount> accounts)
        {
            Person = person;
            Accounts = accounts;
            status = ClientStatus.Active;
            History = new List<Transaction>();
        }
   }

  public enum ClientStatus 
  {
       Blocked,
       Active
  }
}
