using System;
using System.Collections.Generic;
using NUnit.Framework;
using ShopsAssist;

namespace Lab2_test
{
    public class TestShop
    {

        [SetUp()]
        public void Setup()
        {
            Shop shop1 = new Shop(01, "Пятёрочка", "Бульвар 4", "Shop1");
            Starter.MagazinesList.Add(shop1);
            Shop shop2 = new Shop(02, "Магнит", "Соседний бульвар 4", "Shop2");
            Starter.MagazinesList.Add(shop2);
            Shop shop3 = new Shop(03, "Обрыгаловка", "Параллельный бульвар 4", "Shop3");
            Starter.MagazinesList.Add(shop3);
        }

        [Test()]
        public void Test1()
        {
            var res1 = Shop.CalcLot(03, 103, 5);

            Assert.AreEqual(5, res1);
        }

        [Test()]
        public void Test2()
        {

            int res2 = Shop.CalcLot(01, 101, 2);
            Assert.AreEqual(110, res2);
        }

        [Test()]
        public void Test3()
        {
            int res3 = Shop.CalcLot(01, 104, 5);
            Assert.AreEqual(95, res3);
        }

        [Test()]
        public void Test4()
        {
            int res4 = Shop.CalcLot(02, 111, 5);
            Assert.AreEqual(100, res4);
        }

        [Test()]
        public void Test5()
        {
            var res1 = Shop.AloneBuyHelp(103);
            Assert.AreEqual(1, res1);
        }

        [Test()]
        public void Test6()
        {
            var res2 = Shop.AloneBuyHelp(102);
            Assert.AreEqual(300, res2);
        }

        [Test()]
        public void Test7()
        {
            List<string> testList2 = new List<string>();

            testList2.Add("можно купить \"Овощи\", на сумму:80, в количестве: 2");
            testList2.Add("можно купить \"Фрукты\", на сумму:50, в количестве: 1");
            testList2.Add("можно купить \"Ягоды\", на сумму:90, в количестве: 9");

            var res1 = Shop.Bomj(03, 90);
            Assert.AreEqual(testList2, res1);
        }

        [Test()]
        public void Test8()
        {
            List<string> testList1 = new List<string>();

            testList1.Add("можно купить \"Гвозди\", на сумму:50, в количестве: 25");
            testList1.Add("можно купить \"Хлеб\", на сумму:50, в количестве: 2");
            testList1.Add("можно купить \"Конфеты\", на сумму:42, в количестве: 3");
            testList1.Add("можно купить \"Овощи\", на сумму:30, в количестве: 1");
            testList1.Add("можно купить \"Фрукты\", на сумму:35, в количестве: 1");
            testList1.Add("можно купить \"Ягоды\", на сумму:40, в количестве: 2");

            var res1 = Shop.Bomj(02, 50);
            Assert.AreEqual(testList1, res1);
        }
    }
}