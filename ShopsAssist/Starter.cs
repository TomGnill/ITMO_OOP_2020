using System;
using System.Collections.Generic;


namespace ShopsAssist
{
    public class Starter
    {

        public static Dictionary<Shop.Magazine, List<Shop.Product>> MagazinesList = new Dictionary<Shop.Magazine, List<Shop.Product>>();

        static void Main()
        {
            Shop shop1 = new Shop(01, "Пятёрочка", "Бульвар 4", "C:/Users/Андрейка/Source/Repos/TomGnill/ITMO_OOP_2020/ShopsAssist/SupplyList2.txt");
            Shop shop2 = new Shop(02, "Магнит", "Соседний бульвар 4", "C:/Users/Андрейка/Source/Repos/TomGnill/ITMO_OOP_2020/ShopsAssist/SupplyList1.txt");
            Shop shop3 = new Shop(03, "Обрыгаловка", "Параллельный бульвар 4", "C:/Users/Андрейка/Source/Repos/TomGnill/ITMO_OOP_2020/ShopsAssist/SupplyList3.txt");
            while (true)
            {
                Console.WriteLine("1.Операции с магазином");

                Console.WriteLine("2.Делаем покупки выгодными");

                Console.WriteLine("Выбери пункт:");

                int select = Convert.ToInt32(Console.ReadLine());

                switch (select)
                    {
                        case 1 :
                           Console.WriteLine("1.Доставить в магазин партию товаров(необходим список в формате .txt и путь к нему");
                           Console.WriteLine("2.Создать магазин, также необходим список товаров, для открытия");
                           Console.WriteLine("3.Изменить цену на конкретный товар в конкретном магазине");
                           Console.WriteLine("4.Что я можно купить на заданное количество рублей в конкретном магазине");
                           Console.WriteLine("5.Сколько будет стоить партия товаров в конкретном магазине ");
                           Console.WriteLine("Действие:");
                           int command = Convert.ToInt32(Console.ReadLine());
                           switch (command)
                           {
                               case 1:
                                   Console.WriteLine("Введи ID магазина");

                                   int shopID = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("Путь к списку товаров");

                                   string catlog1 = Console.ReadLine();
                                   MenusFitues.addSupplyList(shopID, catlog1);
                                   break;

                               case 2:

                                   Console.WriteLine("ID вашего магазина:");

                                   int newshopID = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("Имя вашего магазина:");

                                   string newshopName = Console.ReadLine();

                                   Console.WriteLine("Адрес вашего магазина:");

                                   string newshopAdress = Console.ReadLine();

                                   Console.WriteLine("путь к списку товаров:");

                                   string catlog2 = Console.ReadLine();

                                   Shop newShop = new Shop(newshopID, newshopName, newshopAdress, catlog2);

                                   break;

                               case 3:
                                   Console.WriteLine("Введите ID магазина");

                                   int ID = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("Введите название продукта");

                                   string ProductName = Console.ReadLine();

                                   Console.WriteLine("Введите новую цену");

                                   int NewPrice = Convert.ToInt32(Console.ReadLine());

                                   MenusFitues.editPrice(ID, ProductName, NewPrice);

                                   break
                                       ;
                               case 4:

                                   Console.WriteLine("ID магазина:");

                                   int shopID2 = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("На какую сумму рассчитывать:");

                                   int wallet = Convert.ToInt32(Console.ReadLine());

                                   MenusFitues.Bomj(shopID2, wallet);

                                   break;
                               case 5:
                                   Console.WriteLine("ID магазина:");

                                   int ShopID3 = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("ID товара:");

                                   int prodID = Convert.ToInt32(Console.ReadLine());

                                   Console.WriteLine("Количество товара:");

                                   int Que = Convert.ToInt32(Console.ReadLine());

                                MenusFitues.CalcLot(ShopID3, prodID, Que);
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
                                MenusFitues.AloneBuyHelp(ProdID);
                                break;
                            case 2: 
                             //   MenusFitues.ListBuyHelp();
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
            // SetPrice pricelist1 = new SetPrice("C:/Users/Андрейка/source/repos/OOP_LABS_AD/Lab_1/SupplyList.txt");
        }
    }
}
