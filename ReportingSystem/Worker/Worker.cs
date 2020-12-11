using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Worker
{
   public class Worker// Слой данных
    {
       public string Name;
       public Worker Chief;
       public List<Worker> SubWorkers;

       public Worker(string name)
       {
           Name = name;
       }
   }
}
