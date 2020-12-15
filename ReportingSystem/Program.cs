using System;
using System.Collections.Generic;
using ReportingSystem.DataAccesslayer.Worker;
using ReportingSystem.Presentationlayer;

namespace ReportingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
           UserCommands Yandex = new UserCommands();
           List<Worker> newWorkers = new List<Worker>();
           
          Worker Kristina = Yandex.CreateWorker("Кристина");
          Worker Andrey = Yandex.CreateWorker("Andrey");
          Worker Fredi = Yandex.CreateWorker("Алексей");
          newWorkers.Add(Kristina);
          newWorkers.Add(Andrey);

          Yandex.GiveWorkeres("Алексей", newWorkers);

          using (WorkerContext db = new WorkerContext())
          {
              var workers = db.Workers;
             Console.WriteLine("Список объектов:");
             foreach (Worker u in workers)
             {
              Console.WriteLine("{0}-{1}", u.Id,u.Name);
             }
          }

          Yandex.PrintWorkersHierarchy();
          


        }
    }
}
