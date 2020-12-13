using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report.Actions
{
   public class FormDayReport
   {
       public Worker.Worker WhoWorkingAllDay;

       public List<TaskInfo> Report;

       public List<TaskInfo> Log;

       public DateTime Date;

       public FormDayReport(Worker.Worker worker, List<TaskInfo> log, DateTime day)
       {
           WhoWorkingAllDay = worker;
           Log = log;
           Date = day;

       }

       public List<TaskInfo> ReturnReports()
       {
           Report = new List<TaskInfo>();
           foreach (var infos in Log)
           {
               if (infos.WhoDoAction == WhoWorkingAllDay && infos.LastEditTime == Date)
               {
                    Report.Add(infos);
               }
           }

           return Report;
       }
   }
}
