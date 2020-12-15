using System;
using System.Collections.Generic;
using ReportingSystem.DataAccesslayer.Report;
using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Businesslayer.FormReport
{
   public class FormFinalReport : IFormReport
   {
       public Worker Boss;
       public List<Worker> Workers;
       public DateTime Date;
       public Report Report;
       public List<TaskInfo> Log;

       public FormFinalReport(Worker boss, DateTime date, List<TaskInfo> infos)
       {
           Boss = boss;
           Workers = boss.SubWorkers;
           Log = infos;
           Date = date;
       }

       public Report GenerateReport()
       {
           List<IFormReport> workerReports = new List<IFormReport>();


           foreach (var t in Workers)
           {
               IFormReport newReport = new FormBossReport(t,Date.Date, Log);
               workerReports.Add(newReport);
           }
           IFormReport bossReport = new FormDayReport(Boss, Log, Date.Date);
           workerReports.Add(bossReport);


           Report = new Report(workerReports);
           using ReportContext db = new ReportContext();
           db.Reports.Add(Report);
            return Report;
       }
    }
}
