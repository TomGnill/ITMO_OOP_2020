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
            Shop shop1 = new Shop(01, "��������", "������� 4", "Shop1");
            Starter.MagazinesList.Add(shop1);
            Shop shop2 = new Shop(02, "������", "�������� ������� 4", "Shop2");
            Starter.MagazinesList.Add(shop2);
            Shop shop3 = new Shop(03, "�����������", "������������ ������� 4", "Shop3");
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

            testList2.Add("����� ������ \"�����\", �� �����:80, � ����������: 2");
            testList2.Add("����� ������ \"������\", �� �����:50, � ����������: 1");
            testList2.Add("����� ������ \"�����\", �� �����:90, � ����������: 9");

            var res1 = Shop.Bomj(03, 90);
            Assert.AreEqual(testList2, res1);
        }

        [Test()]
        public void Test8()
        {
            List<string> testList1 = new List<string>();

            testList1.Add("����� ������ \"������\", �� �����:50, � ����������: 25");
            testList1.Add("����� ������ \"����\", �� �����:50, � ����������: 2");
            testList1.Add("����� ������ \"�������\", �� �����:42, � ����������: 3");
            testList1.Add("����� ������ \"�����\", �� �����:30, � ����������: 1");
            testList1.Add("����� ������ \"������\", �� �����:35, � ����������: 1");
            testList1.Add("����� ������ \"�����\", �� �����:40, � ����������: 2");

            var res1 = Shop.Bomj(02, 50);
            Assert.AreEqual(testList1, res1);
        }
    }
}