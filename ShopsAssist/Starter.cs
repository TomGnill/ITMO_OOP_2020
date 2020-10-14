using System;
using System.Collections.Generic;


namespace ShopsAssist
{
    public class Starter
    {
        
        public static Dictionary<Shop.Magazine, List<Shop.Product>> MagazinesList = new Dictionary<Shop.Magazine, List<Shop.Product>>();
        
        static void Main()
        {
            
            while (true)
            {
                Console.WriteLine("1.Операции с магазином");
                Console.WriteLine("2.Делаем покупки выгодными");
                Console.WriteLine("Выбери пункт:");
                int select = Console.Read();

                switch (select)
                    {
                        case 1 :
                           Console.WriteLine("1.Доставить в магазин партию товаров(необходим список в формате .txt и путь к нему");
                           Console.WriteLine("2.Создать магазин, также необходим список товаров, для открытия");
                           Console.WriteLine("3.Изменить цену на конкретный товар в конкретном магазине");
                           Console.WriteLine("4.Что я можно купить на заданное количество рублей в конкретном магазине");
                           Console.WriteLine("5.Сколько будет стоить партия товаров в конкретном магазине ");
                           Console.WriteLine("Действие:");
                           int command = Console.Read();
                           switch (command)
                           {
                            case 1:
                                Console.WriteLine("Введи ID магазина");
                                int shopID = Console.Read();
                                Console.WriteLine("Путь к списку товаров");
                                string catlog1 = Console.ReadLine();
                                MenusFitues.addSupplyList(shopID,catlog1);
                                break;
                            case 2:
                                
                                Console.WriteLine("ID вашего магазина:");
                                int newshopID = Console.Read();
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
                                int ID = Console.Read();
                                Console.WriteLine("Введите название продукта");
                                string ProductName = Console.ReadLine();
                                Console.WriteLine("Введите новую цену");
                                string newPrice = Console.ReadLine();
                                int NewPrice = Convert.ToInt32(newPrice);
                                MenusFitues.editPrice(ID,ProductName,NewPrice);
                                break
                                    ;
                           }


                            break
                                ;
                        case 2:
                            Console.WriteLine("1.Помогу найти продукт по выгодной цене");
                            Console.WriteLine("2.Помогу купить несколько покупок выгодно");
                            Console.WriteLine("Действие:");
                            int SelectHelp = Console.Read();
                            switch (SelectHelp)
                            {
                            case 1: AloneBuyHelp();
                                break;
                            case 2: ListBuyHelp();
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
