using System.Collections.Generic;
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
            History = new List<Transaction>();
        }
   }

  public enum ClientStatus 
  {
       Blocked,
       Active
  }
}
