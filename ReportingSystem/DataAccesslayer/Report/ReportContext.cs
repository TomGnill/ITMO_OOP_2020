using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ReportingSystem.DataAccesslayer.Report
{
   public class ReportContext  : DbContext
    {
        public ReportContext() : base("ReportConnection")
        { }

        public DbSet<Report> Reports { get; set; }
    }
}
