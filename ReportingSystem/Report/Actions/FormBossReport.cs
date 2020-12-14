using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportingSystem.Report.Actions
{
   public class FormBossReport : IFormReport
   {
       public Worker.Worker Boss;
       public List<Worker.Worker> Workers;
       public DateTime Date;
       public Report Report;
       public List<TaskInfo> Log;

       public FormBossReport(Worker.Worker boss, DateTime date, List<TaskInfo> infos)
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
            return Report;
       }
    }
}
