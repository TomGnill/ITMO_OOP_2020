using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Classes.BankAccount
{
    public class Deposit : BankSystem.BankAccount
    {
        public override double AccountStatus { get; set; }
        public sealed override Guid Id { get; set; }
        public double Sum;
        public override DateTime LastUsing { get; set; }
        public DateTime Validity;
        public override AccountStatus Status { get; set; }

        public double Percent;


        public Deposit(double startStatus, DateTime creatingDate, DateTime validity, double percent)
        {
            AccountStatus = startStatus;
            LastUsing = creatingDate;
            Validity = validity;
            Percent = percent;
            Id = Guid.NewGuid();
            Status = BankSystem.AccountStatus.Sleep;
        }

        public override void ReduceSum(DateTime time)
        {
            double sum;
            double mounth = ((LastUsing - time).Duration().TotalDays) / 30;

            if (time <= Validity && mounth > 1)
            {
                for (int i = 0; i < mounth; i++)
                {
                    sum = AccountStatus * Percent;
                    Sum += sum;
                }
                AccountStatus += Sum;
            }

            if (time >= Validity)
            {
                Status = BankSystem.AccountStatus.Active;

            }
        }
    }
}
