using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Classes.Transactions_Canceling_
{
    public interface ICancelTransaction
    {
        public void Operation();
    }
   public class CancelOperation : ICancelTransaction
    {
        public Client client;
        public int TransactionNumber;
        public CancelOperation(Client theClient, int transactionNumber)
        {
            client = theClient;
            TransactionNumber = transactionNumber;
            Operation();
        }

        public void Operation()
        {
            var transaction = client.History[TransactionNumber];
            if (transaction.Type != Transaction.TrasactionType.Transfer)
            {
                var cancelAc = transaction.Account;
                double cancelSum = transaction.Sum;
                cancelAc.AccountStatus += -1 * cancelSum;
            }
            else
            {
                var canelAcFrst = transaction.Account;
                var cancelAcSec = transaction.AccountSec;

                double cancelSum = transaction.Sum;

                canelAcFrst.AccountStatus += -1 * cancelSum;
                cancelAcSec.AccountStatus += cancelSum;
            }
        }
    }
}
