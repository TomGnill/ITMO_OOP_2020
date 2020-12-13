using System;
using System.Collections.Generic;
using ReportingSystem.Task;

namespace ReportingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
           HelloUser Yandex = new HelloUser();
           List<Worker.Worker> newWorkers = new List<Worker.Worker>();
           
          Worker.Worker Kristina = Yandex.CreateWorker("Кристина");
          Worker.Worker Andrey = Yandex.CreateWorker("Andrey");
          newWorkers.Add(Kristina);
          newWorkers.Add(Andrey);
          Worker.Worker Fredi = Yandex.CreateWorker("Алексей");
          Yandex.GiveWorkeres("Алексей", newWorkers);
        
            Yandex.CreateTask("Лаба1", "Проебать лабу");
            Yandex.CreateTask("Лаба 6", "Написать гитхаб");
            Yandex.ChangeWorker(1, "Кристина");
            Yandex.ChangeWorker(2, "Andrey");
           Yandex.ChangesInCode(1);
           Yandex.ChangesInCode(1);
           Yandex.ChangesInCode(1);
           Yandex.ChangesInCode(2);
           Yandex.ChangesInCode(2);
           Yandex.PrintReport(ReportMode.BossReport, Fredi, DateTime.Now);
         Yandex.PrintLog();
           //Yandex.PrintLog();

        }
    }
}
