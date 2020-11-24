using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
  public abstract class BankAccount
  {
      public abstract double AccountStatus { get; set; }

      public abstract uint Id { get; set; }
        public abstract void ReduceSum(DateTime time);
  }

   public class DebitAccount : BankAccount
   {
       public override double AccountStatus { get; set; }
       public override uint Id { get; set; }
        public double PlusSum;
       public double Percent;
       public DateTime CreatingDate;

       public DebitAccount(double percent, DateTime creatingDate)
       {
           AccountStatus = 0;
           Percent = percent;
           CreatingDate = creatingDate;
       }

       public override void ReduceSum(DateTime time)
       {
           double sum;
           double mounth = ((CreatingDate - time).Duration().TotalDays)/30;

           if (mounth > 1)
           {
               for (int i = 0; i < mounth; i++)
               {
                   sum = AccountStatus * Percent;
                   PlusSum += sum;
               }
               AccountStatus += PlusSum;
           }
       }
   }

   public class Deposit : BankAccount
   {
       public override double AccountStatus { get; set; }
       public override uint Id { get; set; }
        public double Sum;
       public double StartStatus;
       public DateTime CreatingDate;
       public DateTime Validity;
       public List<(double, double)> DepositTerms; //(Сумма ; процент)

       public Deposit(double startStatus, DateTime creatingDate, DateTime validity, List<(double,double)> terms)
       {
           StartStatus = startStatus;
           CreatingDate = creatingDate;
           Validity = validity;
           DepositTerms = terms;
       }

       public override void ReduceSum(DateTime time)
       {

           double Percent = 0 ;
           for (int i = 0; i < DepositTerms.Count; i++)
           {
               if (DepositTerms[i].Item1 > StartStatus)
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
           double mounth = ((CreatingDate - time).Duration().TotalDays) / 30;

            if (time < Validity && mounth > 1)
            {
                for (int i = 0; i < mounth; i++)
                {
                    sum = AccountStatus * Percent;
                    Sum += sum;
                }
                AccountStatus += Sum;
            }
       }
   }

   public class CreditAccount : BankAccount
   {
       public override double AccountStatus { get; set; }
       public override uint Id { get; set; }
        public double Credit;
        public double Percent;
        public DateTime CreatingDate;

        public CreditAccount(double terms, DateTime creatingDate)
        {
            Percent = terms;
            CreatingDate = creatingDate;
        }
        public override void ReduceSum(DateTime time)
        {
            Double PlusCredit;
            double mounth = ((CreatingDate - time).Duration().TotalDays) / 30;
            if (AccountStatus < 0 || Credit > 0 && mounth > 1 )
            {
                for (int i = 0; i < mounth; i++)
                {
                    PlusCredit = Credit * Percent;
                    Credit += PlusCredit;
                }

            }
        }
   }

}
