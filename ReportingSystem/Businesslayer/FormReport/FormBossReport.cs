using System;
using System.Collections.Generic;
using System.Linq;
using ReportingSystem.DataAccesslayer.Report;
using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Businesslayer.FormReport
{
   public class FormBossReport : IFormReport
   {
       public Worker Boss;
       public List<Worker> Workers;
       public DateTime Date;
       public Report Report;
       public List<TaskInfo> Log;

       public FormBossReport(Worker boss, DateTime date, List<TaskInfo> infos)
       {
           Boss = boss;
           Workers = boss.SubWorkers;
           Log = infos;
           Date = date;
       }

       public Report GenerateReport()
       {
            var workerReports = Workers.Select(t => new FormDayReport(t, Log, Date.Date)).Cast<IFormReport>().ToList();


            IFormReport bossReport = new FormDayReport(Boss, Log, Date.Date);
            workerReports.Add(bossReport);


            Report = new Report(workerReports);
            using ReportContext db = new ReportContext();
            db.Reports.Add(Report);
            return Report;
       }
    }
}
