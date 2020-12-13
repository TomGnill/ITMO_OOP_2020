using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report.Actions
{
   public class FormDayReport : IFormReport
   {
       public Worker.Worker WhoWorkingAllDay;

       public Report Report;

       public List<TaskInfo> Log;

       public DateTime Date;

       public FormDayReport(Worker.Worker worker, List<TaskInfo> log, DateTime day)
       {
           WhoWorkingAllDay = worker;
           Log = log;
           Date = day;
           ReturnReport();
       }

       public Report ReturnReport()
       {
           Report = new Report();
           foreach (var infos in Log)
           {
               if (infos.WhoDoAction == WhoWorkingAllDay && infos.LastEditTime == Date.Date)
               {
                    Report.report.Add(infos);
               }
           }

           return Report;
       }
   }
}
