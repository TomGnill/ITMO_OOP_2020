using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Interfaces
{ 
  public  interface IAddAccount
  {
      public void ReduceSum(DateTime time); //Наш механизм накрутки процентов.
  }
}
