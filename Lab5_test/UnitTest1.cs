using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem;
using BankSystem.Classes;
using NUnit.Framework;

namespace Lab5_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //������� �� �������� ��� ������:
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            List<(double, double)> TermForSecondtBank = new List<(double, double)>();

            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            TermForSecondtBank.Add((10000, 0.3));
            TermForSecondtBank.Add((30000, 0.4));

            Bank alfaBank =
                new Bank(TermForfirstBank, 0.1, 0.2, 10000,
                    3000); //������� �� ��������, ��������, ������� �� �������, ��������� �����, ����� ��� �������������� ���������)
            Bank sberBank = new Bank(TermForSecondtBank, 0.2, 0.1, 5000, 1000);

            IAbstractBank firstBank = alfaBank;
            IAbstractBank secondBank = sberBank;

            Adress newAdress = new Adress("��������", 10, "�����");
            Adress additionalAdress = new Adress("��������", 11, "�����");

            PassportData firstPersonData = new PassportData(1111, 22222);
            PassportData secondPersonData = new PassportData(1111, 00000);

            Person Andrey =
                new Person("������", "��������", newAdress, firstPersonData); //�������� ���� ����������� �������.
            Person
                Vasya = new Person("����",
                    "������"); //� ���� ��������������, � ��������� ������ ����� ������������ ��� ������ ���������� � ��������� �����������.

            Client clientAndrey = firstBank.CreateClient(Andrey); //������� ������� � ������ ����

            Client clientVasya = secondBank.CreateClient(Vasya); //���� � ��� ����� �������� �����

        }

        [Test()] //��������� ���������� � ������ ������� � ������
        public void Test1()
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.2, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("��������", 10, "�����");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("������", "��������", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 1000); //���������� ������ �� �������

            Assert.AreEqual(1000, firstAndreyAccount.AccountStatus);

            firstBank.CashWithdrawal(firstAndreyAccount, 200); //������� ������ � �������� 

            Assert.AreEqual(800, firstAndreyAccount.AccountStatus);

            firstBank.CashWithdrawal(firstAndreyAccount,
                1100); //������� ������ ��� �����, ��������� ������� ��������� ������ �� ��������

            Assert.AreEqual(800, firstAndreyAccount.AccountStatus);
        }

        [Test()]
        public void Test2() //��������� ���������� �������� �� ������� // ��������� ����
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("��������", 10, "�����");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("������", "��������", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 1000);


            DateTime futureDateTime = new DateTime(2021, 10, 2);
            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime);
            Assert.AreEqual(1220,
                firstAndreyAccount
                    .AccountStatus); // ������ ����� ����� ���� ����� ���������� �������� �� ������� � ������� 21 ����


            DateTime futureDateTime2 = new DateTime(2021, 12, 2);
            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime2);
            Assert.AreEqual(1293.2, firstAndreyAccount.AccountStatus); // ����� ����� ���� ��� ����� ��� ������.


            Assert.Pass();
        }

        [Test()]
        public void
            Test3() //�������� ���������� �����, ������ ����� ������ ���� sleep �� ������� ��������� ���������� �����
        {
            DateTime futureDateTime = new DateTime(2021, 10, 2);
            DateTime futureDateTime2 = new DateTime(2021, 9, 2);
            DateTime futureDateTime3 = new DateTime(2021, 10, 3);

            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.05));
            TermForfirstBank.Add((20000, 0.10));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("��������", 10, "�����");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("������", "��������", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddDepositAccount(clientAndrey, 300000, DateTime.Now,
                futureDateTime); //������, ��������� �����(��� �� ���������� �������), ���� ������, ���� ���������.
            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime2);

            Assert.AreEqual(600000, firstAndreyAccount.AccountStatus); //��� ���� �� ��������� �� futureDateTime2
            Assert.AreEqual(AccountStatus.Sleep, firstAndreyAccount.Status); // ������ ���� Sleep.

            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime3);
            Assert.AreEqual(AccountStatus.Active, firstAndreyAccount.Status); // ��������� �� Active?
        }

        [Test()] // �������� ����� ����� ��������
        public void Test4()
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            List<(double, double)> TermForSecondtBank = new List<(double, double)>();

            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            TermForSecondtBank.Add((10000, 0.3));
            TermForSecondtBank.Add((30000, 0.4));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.2, 10000, 3000);
            Bank sberBank = new Bank(TermForSecondtBank, 0.2, 0.1, 5000, 1000);

            IAbstractBank firstBank = alfaBank;
            IAbstractBank secondBank = sberBank;

            Adress newAdress = new Adress("��������", 10, "�����");
            Adress additionalAdress = new Adress("��������", 11, "�����");

            PassportData firstPersonData = new PassportData(1111, 22222);
            PassportData secondPersonData = new PassportData(1111, 00000);

            Person Andrey = new Person("������", "��������", newAdress, firstPersonData);
            Person Vasya = new Person("����", "������");

            Client clientAndrey = firstBank.CreateClient(Andrey);
            Client clientVasya = secondBank.CreateClient(Vasya);

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);
            secondBank.AddDebitAccount(clientVasya, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);
            BankAccount firstVasyaAccount = clientVasya.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 2000);
            secondBank.Replenishment(firstVasyaAccount, 100);

            firstBank.Transfer(firstAndreyAccount, firstVasyaAccount, 400,
                sberBank); // ��������� 400 ������ ����, ���� ��� ��� ���� ����������� ���������.

            Assert.AreEqual(1600, firstAndreyAccount.AccountStatus); // ���������� ��� ������ �������� ������
            Assert.AreEqual(500, firstVasyaAccount.AccountStatus); // ���������� ��� ���� ������� ������ 

            firstBank.ReturnMoney(clientAndrey,
                1); //�� ��� ��������� ����� ���������� ��������, ���� ������ ������� �� ������� �������� ������

            Assert.AreEqual(2000, firstAndreyAccount.AccountStatus); // ������ ��������� �������
            Assert.AreEqual(100, firstVasyaAccount.AccountStatus); // � ��� ���� ������ ����������(

        }

        [Test()] //�� ���� ��������� �� ������ ����� �������������� ��������� �������   
        public void Test5()
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("��������", 10, "�����");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("������", "��������", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddCreditAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.CashWithdrawal(firstAndreyAccount, 200);
            Assert.AreEqual(-220, firstAndreyAccount.AccountStatus); //������� 200, �� ��� ���� 0,  �������� 10% ��� -20 �� ��� ����
        }

        [Test()] //���� ��������� ������� 
        public void Test6()
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 1000);

            IAbstractBank firstBank = alfaBank;

            BuildPerson Andrey = new BuildPerson();
            Andrey.AddMainInfo("Anrey", "Loskutov");
            

            Adress newAdress = new Adress("��������", 10, "�����");
            PassportData firstPersonData = new PassportData(1111, 22222);

            Client clientAndrey = firstBank.CreateClient(Andrey.ReturnPersonInfo());

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 2000);
            firstBank.CashWithdrawal(firstAndreyAccount, 1500);
            Assert.AreEqual(2000, firstAndreyAccount.AccountStatus);

            Andrey.AddPassportData(firstPersonData);
            Andrey.AddAdress(newAdress);

            firstBank.RefreshClientInfo(clientAndrey, Andrey.ReturnPersonInfo());//���������� �������� ���� � ������� 

            firstBank.CashWithdrawal(firstAndreyAccount, 1500);
            Assert.AreEqual(500, firstAndreyAccount.AccountStatus);

        }
    }
}