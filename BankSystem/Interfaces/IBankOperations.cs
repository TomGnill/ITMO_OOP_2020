using BankSystem.Classes;

namespace BankSystem.Interfaces
{
    public interface IAccountOperation
    {
        public Client IdentefyClient(BankAccount account);
        public void Operation();
        public Transaction AddTransaction(Client client, Transaction transaction);

    }
}
