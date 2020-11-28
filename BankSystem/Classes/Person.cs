
namespace BankSystem
{
    public class Person
    {
        public string Name;
        public string Surname;
        public Adress PersonAdress;
        public PassportData PersonData;
        public PersonLoyalty Loyalty;

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Loyalty = PersonLoyalty.Doubtful;
        }

        public Person(string name, string surname, Adress adress, PassportData data)
        {
            Name = name;
            Surname = surname;
            PersonAdress = adress;
            PersonData = data;
            Loyalty = PersonLoyalty.Verified;
        }
    }

    public enum PersonLoyalty
    {
        Verified,
        Doubtful
    }
   public  class Adress
   {
       public string Street;
       public uint HouseNumber;
       public string City;

       public Adress(string street, uint houseNumber, string city)
       {
           Street = street;
           HouseNumber = houseNumber;
           City = city;
       }

   }
   public class PassportData
   {
       public uint Series;
       public uint Number;

       public PassportData(uint series, uint number)
       {
           Series = series;
           Number = number;
       }

   }
}
