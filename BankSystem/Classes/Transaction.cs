
namespace BankSystem.Classes
{
   public class Transaction
   {
        public double Sum;
        public BankAccount Account;

        public Transaction(double sum, BankAccount account)
        {
            Sum = sum;
            Account = account;
        }

   }
}
