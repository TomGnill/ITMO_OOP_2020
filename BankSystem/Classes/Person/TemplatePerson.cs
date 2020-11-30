using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Classes.Person
{
    class CreatePerson
    {
        public IBuildPerson build;

        public IBuildPerson builder
        {
            set { build = value; }
        }
        public BankSystem.Person CreateBasePerson(string Name, string Surname)
        {
            this.build.AddMainInfo(Name, Surname);
            return build.ReturnPersonInfo();
        }

        public BankSystem.Person CreateVerefyPerson(string Name, string Surname, Adress personAdress, PassportData personData)
        {
            this.build.AddMainInfo(Name, Surname);
            build.AddAdress(personAdress);
            build.AddPassportData(personData);
            return build.ReturnPersonInfo();
        }
    }
}
