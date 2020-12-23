using System.Collections.Generic;

namespace ReportingSystem.DataAccesslayer.Worker
{
   public class Worker// Слой данных
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Worker Chief { get; set; }
        public List<Worker> SubWorkers { get; set; }
    }
}
