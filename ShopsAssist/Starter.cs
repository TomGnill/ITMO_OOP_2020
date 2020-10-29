using System;
using System.Collections.Generic;
using System.Linq;


namespace ShopsAssist
{
    public class Starter
    {

        public static List<Shop> MagazinesList = new List<Shop>();

        public static void Main()
        {
            Shop shop1 = new Shop(01, "Пятёрочка", "Бульвар 4", "Shop1"); 
            MagazinesList.Add(shop1);
            Shop shop2 = new Shop(02, "Магнит", "Соседний бульвар 4", "Shop2");
            AddMagazine(shop2);
            Shop shop3 = new Shop(03, "Обрыгаловка", "Параллельный бульвар 4", "Shop3");
            AddMagazine(shop3);

            while (true)
            {
                Console.WriteLine("1.Операции с магазином");

                Console.WriteLine("2.Делаем покупки выгодными");

                Console.WriteLine("Выбери пункт:");

                int select = Convert.ToInt32(Console.ReadLine());

                switch (select)
                    {
                        case 1 :
                            Console.WriteLine("1.Создать магазин, также необходим список товаров, для открытия");
                           Console.WriteLine("2.Изменить цену на конкретный товар в конкретном магазине");
                           Console.WriteLine("3.Что я можно купить на заданное количество рублей в конкретном магазине");
                           Console.WriteLine("4. Сколько будет стоить партия товаров в конкретном магазине ");
                            Console.WriteLine("5. получить список магазинов ");
                            Console.WriteLine("6. получить список товаров  ");
                            Console.WriteLine("Действие:");
                           int command = Convert.ToInt32(Console.ReadLine());
                           switch (command)
                           {
                               case 1:

                                   Console.WriteLine("ID вашего магазина:");

                                   int newshopID = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("Имя вашего магазина:");

                                   string newshopName = Console.ReadLine();

                                   Console.WriteLine("Адрес вашего магазина:");

                                   string newshopAdress = Console.ReadLine();

                                   Console.WriteLine("Название страницы с товарами в файле PriseList.xlsx:");

                                   string catlog2 = Console.ReadLine();

                                   Shop newShop = new Shop(newshopID, newshopName, newshopAdress, catlog2);
                                   MagazinesList.Add(newShop);

                                   break;

                               case 2:
                                   Console.WriteLine("Введите ID магазина");

                                   int ID = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("Введите название продукта");

                                   string ProductName = Console.ReadLine();

                                   Console.WriteLine("Введите новую цену");

                                   int NewPrice = Convert.ToInt32(Console.ReadLine());

                                   Shop.EditPrice(ID, ProductName, NewPrice);

                                   break
                                       ;
                               case 3:

                                   Console.WriteLine("ID магазина:");

                                   int shopID2 = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("На какую сумму рассчитывать:");

                                   int wallet = Convert.ToInt32(Console.ReadLine());

                                   Shop.Bomj(shopID2, wallet);

                                   break;
                               case 4:
                                   Console.WriteLine("ID магазина:");

                                   int ShopID3 = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("ID товара:");

                                   int prodID = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("Количество товара:");

                                   int Que = Convert.ToInt32(Console.ReadLine());
                                   
                                   int sum = Shop.CalcLot(ShopID3, prodID, Que);
                                   Console.WriteLine($"Сумма покупки:{sum}");
                                   break;
                            case 5: 
                                ShowMagazinesList();
                                break;
                            case 6:
                                Console.WriteLine("Введите ID магазина");
                                int ShopID = Convert.ToInt32(Console.ReadLine());
                                ShowPriceList(ShopID);
                                break;
                           }


                           break
                                ;
                        case 2:
                            Console.WriteLine("1.Помогу найти продукт по выгодной цене");
                            Console.WriteLine("2.Помогу купить несколько покупок выгодно");
                            Console.WriteLine("Действие:");
                            int SelectHelp = Convert.ToInt32(Console.ReadLine());
                            switch (SelectHelp)
                            {
                            case 1:
                                Console.WriteLine("введите ID товара");
                                int ProdID = Convert.ToInt32(Console.ReadLine());
                                
                                Shop.LowestCost(ProdID);
                                break;
                            case 2: 
                                Console.WriteLine("Введите имена товаров, когда закончите введите stop ");
                                
                                List< (string,int)> shopping= new List<(string, int)>();
                                int que;
                                string name;
                                while (true)
                                {
                                    Console.WriteLine("Имя товара:");
                                    name = Console.ReadLine();
                                    if (name == "stop")
                                    { 
                                       int answer =  Shop.ListBuyHelp(shopping);
                                        Console.WriteLine($"Сумма покупки:{answer}");
                                        break;
                                    }
                                    Console.WriteLine("Количество :");
                                    que = Convert.ToInt32(Console.ReadLine());
                                    shopping.Add((name,que));

                                }
                              
                                break;
                            default : Console.WriteLine("Такого действия нет");
                                break;
                            }
                            break;

                 

                        default:
                            {
                                Console.WriteLine("Пункт не выбран");
                                Main();
                            }
                            break;
                    }
                
            }
           
        }

        public static void ShowMagazinesList()
        {
            foreach (Shop magazine in Starter.MagazinesList)
            {
                Console.WriteLine("//Уникальный номер:" + magazine.ShopId + "// Имя магазина: " + magazine.ShopName +
                                  " //Адрес: " +
                                  magazine.Adress);
            }
        }


        public static void ShowPriceList(int id)
        {
            foreach (var aProduct in MagazinesList.Where(magazine => magazine.ShopId == id).SelectMany(magazine => magazine.Products))
            {
                Console.WriteLine("//Уникальный номер:" + aProduct.ProductId + "// Имя продукта " + aProduct.ProductName + " //Цена: " + aProduct.ProductPrice + "//Количество :" + aProduct.ProductQuantity);
            }
        }

        public static void AddMagazine(Shop newShop)
        {
            foreach (Shop shop in MagazinesList)
            {
                if (shop.ShopId != newShop.ShopId)
                {
                    MagazinesList.Add(newShop);break;
                }
            }
        }
    }
}
