using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Classes.BankAccount
{
    public class CreditAccount : BankSystem.BankAccount
    {
        public override double AccountStatus { get; set; }
        public sealed override Guid Id { get; set; }
        public double Credit { get; set; }
        public double Percent;
        public double limit;
        public override AccountStatus Status { get; set; }
        public override DateTime LastUsing { get; set; }

        public CreditAccount(double terms, DateTime creatingDate, double maxLimit)
        {
            Percent = terms;
            LastUsing = creatingDate;
            Id = Guid.NewGuid();
            limit = maxLimit;
            Status = BankSystem.AccountStatus.Active;
        }
        public override void ReduceSum(DateTime time)
        {
            if (AccountStatus < 0)
            {
                Credit += -1 * AccountStatus;
            }
            if (limit < Credit)
            {
                Status = BankSystem.AccountStatus.Sleep;
            }

            if (limit > Credit && Status == BankSystem.AccountStatus.Sleep)
            {
                Status = BankSystem.AccountStatus.Active;
            }

        }
    }
}
