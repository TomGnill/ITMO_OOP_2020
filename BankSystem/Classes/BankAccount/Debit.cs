using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Classes.BankAccount
{
    public class DebitAccount : BankSystem.BankAccount
    {
        public override double AccountStatus { get; set; }
        public sealed override Guid Id { get; set; }
        public double PlusSum;
        public double Percent;
        public override DateTime LastUsing { get; set; }
        public override AccountStatus Status { get; set; }

        public DebitAccount(double percent, DateTime creatingDate)
        {
            AccountStatus = 0;
            Percent = percent;
            LastUsing = creatingDate;
            Id = Guid.NewGuid();
            Status = BankSystem.AccountStatus.Active;
        }

        public override void ReduceSum(DateTime time)
        {
            double sum;
            double mounth = ((LastUsing - time).Duration().TotalDays) / 30;

            LastUsing = time;
            if (mounth > 1)
            {
                for (int i = 0; i < mounth; i++)
                {
                    sum = AccountStatus * Percent;
                    PlusSum += sum;
                }
                AccountStatus += PlusSum;
                PlusSum = 0;
            }
        }
    }
}
