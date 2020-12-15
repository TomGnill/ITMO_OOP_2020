using System;
using System.Linq;
using NUnit.Framework;
using ReportingSystem;
using ReportingSystem.Businesslayer.FormReport;
using ReportingSystem.DataAccesslayer.Task;
using ReportingSystem.DataAccesslayer.Worker;
using ReportingSystem.Presentationlayer;
using ReportingSystem.Task;

namespace Lab6_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test()]
        public void Test1()
        {
            UserCommands Yandex = new UserCommands();
            Worker Andrey = Yandex.CreateWorker("Andrey");
            Yandex.CreateTask("Лаба1", "написать парсер");
            Yandex.ChangeWorker(1, "Andrey");
            Task tasks = Yandex.NewSystem.TaskList.ElementAt(0).Key;
            Assert.AreEqual(TaskStatus.Open, tasks.Status);
            Yandex.ChangesInCode(1);
           // Yandex.ChangeTaskStatus(1, TaskStatus.Active);
            Task tasks2 = Yandex.NewSystem.TaskList.ElementAt(0).Key;
            Assert.AreEqual(TaskStatus.Active ,tasks2.Status);
        }
        [Test()]
        public void Test2()
        {
            UserCommands Yandex = new UserCommands();
            Worker Andrey = Yandex.CreateWorker("Andrey");
            Yandex.CreateTask("Лаба1", "написать парсер");
            Yandex.ChangeWorker(1, "Andrey");
            Task tasks = Yandex.NewSystem.TaskList.ElementAt(0).Key;
            Assert.AreEqual(TaskStatus.Open, tasks.Status);
            Yandex.ChangesInCode(1);
            Yandex.ChangesInCode(1);
           IFormReport newReport = Yandex.NewSystem.FormDayReport(Andrey, DateTime.Now);
            Assert.AreEqual(2, newReport.GenerateReport().report.Count);
        }
        [Test()]
        public void Test3()
        {
            UserCommands Yandex = new UserCommands();
            Worker Andrey = Yandex.CreateWorker("Andrey");
            Yandex.CreateTask("Лаба1", "написать парсер");
            Yandex.ChangeWorker(1, "Andrey");
            Task tasks = Yandex.NewSystem.TaskList.ElementAt(0).Key;
            Assert.AreEqual(TaskStatus.Open, tasks.Status);
            Yandex.ChangesInCode(1);
            Yandex.ChangesInCode(1);
            IFormReport newReport = Yandex.NewSystem.FormDayReport(Andrey, DateTime.Now);
            Assert.AreEqual(2, newReport.GenerateReport().report.Count);
        }

    }
}