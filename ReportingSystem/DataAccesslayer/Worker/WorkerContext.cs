using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ReportingSystem.DataAccesslayer.Worker
{
    public class WorkerContext : DbContext
    {
        public WorkerContext() : base("WorkerConnection")
        { }

        public DbSet<Worker> Workers { get; set; }
    }
}
