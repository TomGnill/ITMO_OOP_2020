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
            //Условия по депозиту для банков:
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            List<(double, double)> TermForSecondtBank = new List<(double, double)>();

            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            TermForSecondtBank.Add((10000, 0.3));
            TermForSecondtBank.Add((30000, 0.4));

            Bank alfaBank =
                new Bank(TermForfirstBank, 0.1, 0.2, 10000,
                    3000); //Условия по депозиту, комиссия, процент на остаток, кредитный лимит, лимит для подозрительных аккаунтов)
            Bank sberBank = new Bank(TermForSecondtBank, 0.2, 0.1, 5000, 1000);

            IAbstractBank firstBank = alfaBank;
            IAbstractBank secondBank = sberBank;

            Adress newAdress = new Adress("Невского", 10, "Питер");
            Adress additionalAdress = new Adress("Невского", 11, "Питер");

            PassportData firstPersonData = new PassportData(1111, 22222);
            PassportData secondPersonData = new PassportData(1111, 00000);

            Person Andrey =
                new Person("Андрей", "Загудько", newAdress, firstPersonData); //Создадим одну проверенную персону.
            Person
                Vasya = new Person("Вася",
                    "Дударь"); //И одну подозрительную, в некоторых тестах будем подтверждать его статус лояльности и проверять ограничения.

            Client clientAndrey = firstBank.CreateClient(Andrey); //добавим Андрюху в первый банк

            Client clientVasya = secondBank.CreateClient(Vasya); //Вася у нас будет клиентом Сбера

        }

        [Test()] //Тестируем пополнение и снятие средств с счетов
        public void Test1()
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.2, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("Невского", 10, "Питер");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("Андрей", "Загудько", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 1000); //Закидываем деньги на аккаунт

            Assert.AreEqual(1000, firstAndreyAccount.AccountStatus);

            firstBank.CashWithdrawal(firstAndreyAccount, 200); //Снимаем деньги с аккаунта 

            Assert.AreEqual(800, firstAndreyAccount.AccountStatus);

            firstBank.CashWithdrawal(firstAndreyAccount,
                1100); //Снимаем больше чем имеем, поскольку аккаунт дебитовый деньги не снимутся

            Assert.AreEqual(800, firstAndreyAccount.AccountStatus);
        }

        [Test()]
        public void Test2() //Проверяем начисление процента на остаток // Дебитовый счёт
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("Невского", 10, "Питер");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("Андрей", "Загудько", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 1000);


            DateTime futureDateTime = new DateTime(2021, 10, 2);
            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime);
            Assert.AreEqual(1220,
                firstAndreyAccount
                    .AccountStatus); // узнаем какой будет счёт после начисления процента на остаток в октябре 21 года


            DateTime futureDateTime2 = new DateTime(2021, 12, 2);
            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime2);
            Assert.AreEqual(1293.2, firstAndreyAccount.AccountStatus); // Какой будет счёт ещё через два месяца.


            Assert.Pass();
        }

        [Test()]
        public void
            Test3() //Проверим депозитные счета, статус счёта должен быть sleep до момента окончания начисления денег
        {
            DateTime futureDateTime = new DateTime(2021, 10, 2);
            DateTime futureDateTime2 = new DateTime(2021, 9, 2);
            DateTime futureDateTime3 = new DateTime(2021, 10, 3);

            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.05));
            TermForfirstBank.Add((20000, 0.10));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("Невского", 10, "Питер");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("Андрей", "Загудько", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddDepositAccount(clientAndrey, 300000, DateTime.Now,
                futureDateTime); //клиент, стартовая сумма(она же определяет процент), дата начала, дата окончания.
            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime2);

            Assert.AreEqual(600000, firstAndreyAccount.AccountStatus); //наш счёт по состоянию на futureDateTime2
            Assert.AreEqual(AccountStatus.Sleep, firstAndreyAccount.Status); // должен быть Sleep.

            firstBank.RefreshInfoAboutAccount(firstAndreyAccount, futureDateTime3);
            Assert.AreEqual(AccountStatus.Active, firstAndreyAccount.Status); // Изменился на Active?
        }

        [Test()] // Проверим откат наших операций
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

            Adress newAdress = new Adress("Невского", 10, "Питер");
            Adress additionalAdress = new Adress("Невского", 11, "Питер");

            PassportData firstPersonData = new PassportData(1111, 22222);
            PassportData secondPersonData = new PassportData(1111, 00000);

            Person Andrey = new Person("Андрей", "Загудько", newAdress, firstPersonData);
            Person Vasya = new Person("Вася", "Дударь");

            Client clientAndrey = firstBank.CreateClient(Andrey);
            Client clientVasya = secondBank.CreateClient(Vasya);

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);
            secondBank.AddDebitAccount(clientVasya, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);
            BankAccount firstVasyaAccount = clientVasya.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 2000);
            secondBank.Replenishment(firstVasyaAccount, 100);

            firstBank.Transfer(firstAndreyAccount, firstVasyaAccount, 400,
                sberBank); // Переводим 400 рублей Васе, зная что его счёт принадлежит сбербанку.

            Assert.AreEqual(1600, firstAndreyAccount.AccountStatus); // убеждаемся что Андрей отправил деньги
            Assert.AreEqual(500, firstVasyaAccount.AccountStatus); // Убеждаемся что Вася получил деньги 

            firstBank.ReturnMoney(clientAndrey,
                1); //тк это следующая после пополнения операция, берём первый элемент из истории операции Андрея

            Assert.AreEqual(2000, firstAndreyAccount.AccountStatus); // Деньги вернулись обратно
            Assert.AreEqual(100, firstVasyaAccount.AccountStatus); // а вот Васю кинули получается(

        }

        [Test()] //по всей видимости не лишним будет протестировать кредитный аккаунт   
        public void Test5()
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 3000);

            IAbstractBank firstBank = alfaBank;

            Adress newAdress = new Adress("Невского", 10, "Питер");
            PassportData firstPersonData = new PassportData(1111, 22222);
            Person Andrey = new Person("Андрей", "Загудько", newAdress, firstPersonData);

            Client clientAndrey = firstBank.CreateClient(Andrey);

            firstBank.AddCreditAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.CashWithdrawal(firstAndreyAccount, 200);
            Assert.AreEqual(-220, firstAndreyAccount.AccountStatus); //снимали 200, тк наш счёт 0,  комиссия 10% ещё -20 на наш счёт
        }

        [Test()] //тест строителя персоны 
        public void Test6()
        {
            List<(double, double)> TermForfirstBank = new List<(double, double)>();
            TermForfirstBank.Add((10000, 0.1));
            TermForfirstBank.Add((20000, 0.2));

            Bank alfaBank = new Bank(TermForfirstBank, 0.1, 0.02, 10000, 1000);

            IAbstractBank firstBank = alfaBank;

            BuildPerson Andrey = new BuildPerson();
            Andrey.AddMainInfo("Anrey", "Loskutov");
            

            Adress newAdress = new Adress("Невского", 10, "Питер");
            PassportData firstPersonData = new PassportData(1111, 22222);

            Client clientAndrey = firstBank.CreateClient(Andrey.ReturnPersonInfo());

            firstBank.AddDebitAccount(clientAndrey, DateTime.Now);

            BankAccount firstAndreyAccount = clientAndrey.Accounts.ElementAt(0);

            firstBank.Replenishment(firstAndreyAccount, 2000);
            firstBank.CashWithdrawal(firstAndreyAccount, 1500);
            Assert.AreEqual(2000, firstAndreyAccount.AccountStatus);

            Andrey.AddPassportData(firstPersonData);
            Andrey.AddAdress(newAdress);

            firstBank.RefreshClientInfo(clientAndrey, Andrey.ReturnPersonInfo());//Необходимо обновить инфу о клиенте 

            firstBank.CashWithdrawal(firstAndreyAccount, 1500);
            Assert.AreEqual(500, firstAndreyAccount.AccountStatus);

        }
    }
}