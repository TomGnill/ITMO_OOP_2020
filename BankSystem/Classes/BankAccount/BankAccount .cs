using System;
using System.Collections.Generic;
using BankSystem.Interfaces;

namespace BankSystem
{
  
    public abstract class BankAccount : IAddAccount //идею понял, но переделывать не буду, так как ссылок много.
    {
        public abstract double AccountStatus { get; set; }
        public abstract DateTime LastUsing { get; set; }
        public abstract Guid Id { get; set; } 
        public abstract void ReduceSum(DateTime time);
        public abstract AccountStatus Status { get; set; }
    }
    public enum AccountStatus
   {
       Active,
       Sleep
   }
}
