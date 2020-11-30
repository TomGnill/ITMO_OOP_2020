
namespace BankSystem.Classes
{
   public class Transaction
   {    //создать интерфейс отмены операции и привязать его сюда
        public double Sum;
        public BankSystem.BankAccount Account;
        public BankSystem.BankAccount AccountSec;
        public TrasactionType Type;

        public Transaction(double sum, BankSystem.BankAccount account, TrasactionType type)
        {
            Sum = sum;
            Account = account;
            Type = type;
        }

        public Transaction(double sum, BankSystem.BankAccount account, BankSystem.BankAccount accountSec)
        {
            Sum = sum;
            Account = account;
            AccountSec = accountSec;
            Type = TrasactionType.Transfer;
        }
        public enum TrasactionType
        {
            Transfer,
            Replenishment,
            CashWithdrawal
        }

   }
}
