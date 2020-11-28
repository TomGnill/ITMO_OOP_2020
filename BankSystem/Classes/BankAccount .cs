using System;
using System.Collections.Generic;
using BankSystem.Interfaces;

namespace BankSystem
{
  
    public abstract class BankAccount : IAddAccount
    {
      public abstract double AccountStatus { get; set; }
      public abstract DateTime LastUsing { get; set; }
        public abstract Guid Id { get; set; }
      public abstract void ReduceSum(DateTime time);
      public abstract AccountStatus Status { get; set; }
    }

    public class DebitAccount : BankAccount
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
           double mounth = ((LastUsing - time).Duration().TotalDays)/30;

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

   public class Deposit : BankAccount
   {
       public override double AccountStatus { get; set; }
       public sealed override Guid Id { get; set; }
       public double Sum;
       public double StartStatus;
       public override DateTime LastUsing { get; set; }
       public DateTime Validity;
       public List<(double, double)> DepositTerms; //(Сумма начальная ; процент)
       public override AccountStatus Status { get; set; }


        public Deposit(double startStatus, DateTime creatingDate, DateTime validity, List<(double,double)> terms)
        { 
            AccountStatus = startStatus;
           StartStatus = startStatus;
           LastUsing = creatingDate;
           Validity = validity;
           DepositTerms = terms;
           Id = Guid.NewGuid();
           Status = BankSystem.AccountStatus.Sleep;
        }

       public override void ReduceSum(DateTime time)
       {

           double Percent = 0 ;
           for (int i = DepositTerms.Count -1 ; i >=0 ; i--)
           {
               if (DepositTerms[i].Item1 < StartStatus)
               {
                   Percent = DepositTerms[i].Item2;
                   break;
               }
           }

           if (Percent == 0)
           {
               Percent = DepositTerms[0].Item2;
           }

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

   public class CreditAccount : BankAccount
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
        }
   }

   public enum AccountStatus
   {
       Active,
       Sleep
   }
}
