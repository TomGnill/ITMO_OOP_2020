
namespace BankSystem.Classes
{
   public class Transaction
   {
        public double Sum;
        public BankAccount Account;
        public BankAccount AccountSec;
        public TrasactionType Type;

        public Transaction(double sum, BankAccount account, TrasactionType type)
        {
            Sum = sum;
            Account = account;
            Type = type;
        }

        public Transaction(double sum, BankAccount account,BankAccount accountSec)
        {
            Sum = sum;
            Account = account;
            AccountSec = accountSec;
        }
        public enum TrasactionType
        {
            Transfer,
            Replenishment,
            CashWithdrawal
        }

   }
}
