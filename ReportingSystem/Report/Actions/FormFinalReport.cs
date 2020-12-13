﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report.Actions
{
   public class FormFinalReport : IFormReport
   {
       public Worker.Worker Boss;
       public List<Worker.Worker> Workers;
       public DateTime Date;
       public Report Report;
       public List<TaskInfo> Log;

       public FormFinalReport(Worker.Worker boss, DateTime date, List<TaskInfo> infos)
       {
           Boss = boss;
           Workers = boss.SubWorkers;
           Log = infos;
           Date = date;
       }

       public Report ReturnReport()
       {
           List<IFormReport> workerReports = new List<IFormReport>();


           for (int i = 0; i < Workers.Count; i++)
           {
               IFormReport newReport = new FormBossReport(Workers[i],Date.Date, Log);
               workerReports.Add(newReport);
           }
           IFormReport bossReport = new FormDayReport(Boss, Log, Date.Date);
           workerReports.Add(bossReport);


           Report = new Report(workerReports);
           return Report;
       }
    }
}