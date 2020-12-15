using System;
using System.Collections.Generic;
using System.Linq;
using ReportingSystem.DataAccesslayer.Report;
using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Businesslayer.FormReport
{
   public class FormDayReport : IFormReport
   {
       public Worker WhoWorkingAllDay;// нейминг мудачий я понял!!!!!!

       public List<TaskInfo> Log;

       public DateTime Date;

       public FormDayReport(Worker worker, List<TaskInfo> log, DateTime day)
       {
           WhoWorkingAllDay = worker;
           Log = log;
           Date = day;
       }

       public Report GenerateReport()
       {
           var Report = new Report();
           foreach (var infos in Log.Where(infos => infos.Person == WhoWorkingAllDay && infos.LastEditTime == Date.Date))
           {
               Report.report.Add(infos);
           }

           using (ReportContext db = new ReportContext())
           {
               db.Reports.Add(Report);
           }

           return Report;
       }
   }
}
