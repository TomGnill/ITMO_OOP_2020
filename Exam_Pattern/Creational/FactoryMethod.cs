namespace Exam_Pattern.Creational
{
   /*  abstract class Factory
    {
        public  abstract  IProduct Product();

        public int GetPrice()
        {
            var product = Product();

            var price = product.GetPrice();

            return price;
        }
    }

     class SweetsFactory : Factory
     {
        public override IProduct Product()
        {
            return new Sweets();
        }
     }

     class ChocolateFactory : Factory
     {
         public override IProduct Product()
         { 
             return new Chocolate();
         }
     }


     interface IProduct
    {
        public int GetPrice();
    }
     class Sweets : IProduct
    {
        public int Price = 2;

        public int GetPrice()
        {
            return Price;
        }

    }

     class Chocolate : IProduct
    {
        public int Price = 4;

        public int GetPrice()
        {
            return Price;
        }
    }

     class Client
     {
        public void Main()
        {
            Console.WriteLine("Запуск фабрики шоколадок");
            ClietCode(new ChocolateFactory());

            Console.WriteLine("Запуск фабрики конфеток");
            ClietCode(new SweetsFactory());
        }
        public void ClietCode(Factory factory)
        {
            Console.WriteLine("Цена продукта" + factory.GetPrice());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
   */
}
