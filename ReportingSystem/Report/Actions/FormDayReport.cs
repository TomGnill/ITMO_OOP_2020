using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportingSystem.Report.Actions
{
   public class FormDayReport : IFormReport
   {
       public Worker.Worker WhoWorkingAllDay;// нейминг мудачий я понял!!!!!!

       public List<TaskInfo> Log;

       public DateTime Date;

       public FormDayReport(Worker.Worker worker, List<TaskInfo> log, DateTime day)
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

           return Report;
       }
   }
}
