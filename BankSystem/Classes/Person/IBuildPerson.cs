using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace BankSystem.Classes
{
    interface IBuildPerson
    {
        //дописать строителя
        public void AddMainInfo(string Name, string Surname);

        public void AddAdress(Adress personAdress);

        public void AddPassportData(PassportData personData);

        public BankSystem.Person ReturnPersonInfo();
    }

    public class BuildPerson : IBuildPerson
    { 
       public BankSystem.Person newPerson = new BankSystem.Person();

       public BuildPerson()
       {
           this.Reset();
       }

       public  void Reset()
       {
           this.newPerson = new BankSystem.Person();
       }

       public void AddMainInfo(string Name, string Surname)
       {
           this.newPerson = new BankSystem.Person(Name, Surname);
       }

       public void AddAdress(Adress personAdress)
       {
           this.newPerson.PersonAdress = personAdress;
       }

       public void AddPassportData(PassportData data)
       {
           this.newPerson.PersonData = data;
       }

       public BankSystem.Person ReturnPersonInfo()
       {
           BankSystem.Person ourPerson = this.newPerson;
           Reset();
           return ourPerson;
       }
    }
    
}
