using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopsAssist
{
    class Starter
    {
        public static Dictionary<Shop.Magazine, Dictionary<SetPrice.Product, int>> MagazinesList = new Dictionary<Shop.Magazine, Dictionary<SetPrice.Product, int>>();


        enum selectGlobal
        {
            magazines = 1,
            helper,
        };

        enum selectMagazines
        {
            Supply = 1,
            ShowPriceList,
            SelectProduct,
            BuyHelp,
            Cardlist,
        };

        enum selectHelp
        {
            ShowList = 1,
            Hepler,

        };

        static void MenuGl(selectGlobal commands)
        {
            switch (commands)
            {
                case selectGlobal.magazines:

                    Console.WriteLine("Выбери магазин:")
                    ;
                    break;
                case selectGlobal.helper: 
                    Console.WriteLine("1.Найти товар по самой выгодной цене");
                    Console.WriteLine("2.Где купить набор товаров выгоднее всего");
                    int helpSelect = Console.Read();
                    if (helpSelect == 1)
                    {
                        MenuHl(selectHelp.Hepler);
                    }

                    if (helpSelect == 2)
                    {
                        MenuHl(selectHelp.ShowList);
                    }
                    break;
                default: Console.WriteLine("Вы выбрали несуществующее действие");
                    break;
            }
        }

        static void MenuHl(selectHelp commands)
        {
            //дописать методы высчитывания 
        }

        static void MenuSh(selectMagazines commands)
        {
            //сделаю список магазинов 
        }
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1.Список магазинов");
                Console.WriteLine("2.Делаем покупки выгодными");
                Console.WriteLine("Выбери пункт:");
                int select = Console.Read();
                if (select == 1)
                {
                    MenuGl(selectGlobal.magazines);
                }

                if (select == 2)
                {
                    MenuGl(selectGlobal.helper);
                }

                else
                {
                    Console.WriteLine("Пункт не выбран");
                    Main();
                }
            }
            // SetPrice pricelist1 = new SetPrice("C:/Users/Андрейка/source/repos/OOP_LABS_AD/Lab_1/SupplyList.txt");
        }
    }
}
