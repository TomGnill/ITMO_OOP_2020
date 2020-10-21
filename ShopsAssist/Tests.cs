using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShopsAssist
{
    class Tests
    {
        public static void Search()
        {
            Console.WriteLine("[Узнаём цену на количество товара в магазине]");

            Console.WriteLine("Тест1.входные данные 03 103 5");
            int res1 = Shop.CalcLot(03, 103, 5);
            Trace.Assert(res1 == 5, "Search 103");

            Console.WriteLine("Тест2.входные данные 01 101 2");
            int res2 = Shop.CalcLot(01, 101, 2);
            Debug.Assert(res2 == 110, "Search 103");

            Console.WriteLine("Тест3.входные данные 01 104 5");
            int res3 = Shop.CalcLot(01, 104, 5);
            Debug.Assert(res3 == 95, "Search 103");

            Console.WriteLine("Тест4.входные данные 02 111 5");
            int res4 = Shop.CalcLot(02, 111, 5);
            Debug.Assert(res4 == 100, "Search 103");

        }

        public  static void AloneBuy()
        {
            Console.WriteLine("[Ищем товар по самой дешёвой цене]");

            Console.WriteLine("Тест1.Входные данные 103");
            var res1 = Shop.AloneBuyHelp(103);
            Debug.Assert(res1 == 1, "AloneBuyHelp");

            Console.WriteLine("Тест2.Входные данные 102");
            var res2 = Shop.AloneBuyHelp(102);
            Debug.Assert(res2 == 300, "AloneBuyHelp");

            Console.WriteLine("Тест3.Входные данные 101");
            var res3 = Shop.AloneBuyHelp(101);
            Debug.Assert(res3 == 54, "AloneBuyHelp");

            Console.WriteLine("Тест4.Входные данные 106");
            var res4 = Shop.AloneBuyHelp(106);
            Debug.Assert(res4 == 100, "AloneBuyHelp");


        }

        public static void bomj()
        {
            Console.WriteLine("[Что можно купить на .... рублей]");
            Console.WriteLine("Тест1.Входные данные 02 50:");
            List<string> testList1 = new List<string>();

           testList1.Add("можно купить \"Гвозди\", на сумму:50, в количестве: 25");
           testList1.Add(" можно купить \"Хлеб\", на сумму:50, в количестве: 2");
           testList1.Add( "можно купить \"Конфеты\", на сумму:42, в количестве: 3");
           testList1.Add("можно купить \"Овощи\", на сумму:30, в количестве: 1");
           testList1.Add("можно купить \"Фрукты\", на сумму:35, в количестве: 1");
           testList1.Add("можно купить \"Ягоды\", на сумму:40, в количестве: 2");

            var res1 = Shop.Bomj(02, 50);
            Debug.Assert(res1 == testList1, "bomj");

            Console.WriteLine("Тест2.Входные данные 03 90:");

            List<string> testList2 = new List<string>();

            testList2.Add(" можно купить \"Овощи\"    , на сумму:80, в количестве: 2");
            testList2.Add(" можно купить \"Фрукты\", на сумму:50, в количестве: 1");
            testList2.Add(" можно купить \"Ягоды\"    , на сумму:90, в количестве: 9");

            var res2 = Shop.Bomj(03, 90);
            Debug.Assert(res2 == testList2, "bomj");

        }

        public static void ListBuyHelp()
        {
            Console.WriteLine("[Помогаем найти минимальную цену пакета товаров среди магазинов].");
            Console.WriteLine("Тест1.Список покупок(продукт, количество):Конфеты(5),Консерва(3),Молоко(5)");
            List<(string, int)> testShopping1 = new List<(string, int)>();

            testShopping1.Add(("Конфеты", 5));
            testShopping1.Add(("Консерва", 3));
            testShopping1.Add(("Молоко", 5));

            var res1 = Shop.ListBuyHelp(testShopping1);
            Debug.Assert(res1 == 790, "ok!");

            Console.WriteLine("Тест2.Список покупок(продукт, количество):Молоко(4), Гвозди(9), Сметана(2), Хлеб(2), Колбаса(2)");
            List<(string, int)> testShopping2 = new List<(string, int)>();

            testShopping2.Add(("Молоко", 4));
            testShopping2.Add(("Гвозди", 9));
            testShopping2.Add(("Сметана", 2));
            testShopping2.Add(("Хлеб", 2));
            testShopping2.Add(("Колбаса", 2));
            var res2 = Shop.ListBuyHelp(testShopping2);
            Debug.Assert(res2 == 1124, "ok!");


        }
    }
}

