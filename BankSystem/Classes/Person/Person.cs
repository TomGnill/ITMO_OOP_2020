
namespace BankSystem
{
    public class Person
    {
        readonly string Name;
        readonly string Surname;
        internal Adress PersonAdress;
        internal PassportData PersonData;

        public Person() { }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public Person(string name, string surname, Adress adress, PassportData data)
        {
            Name = name;
            Surname = surname;
            PersonAdress = adress;
            PersonData = data;
        }

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
