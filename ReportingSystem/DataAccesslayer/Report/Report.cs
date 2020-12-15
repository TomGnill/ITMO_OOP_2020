using System;
using System.Data.Entity;
using System.Collections.Generic;
using ReportingSystem.Businesslayer.FormReport;

namespace ReportingSystem.DataAccesslayer.Report
{
    public class Report// Слой данных
    {
        public List<TaskInfo> report;
        public Report()// за день
        {
            report = new List<TaskInfo>();
        }

        public Report(List<IFormReport> reports)//спринт
        {
            report = new List<TaskInfo>();
            foreach (var repos in reports)
            {
                if (repos.GenerateReport().report != null)
                {
                    for (int i = 0; i < repos.GenerateReport().report.Count; i++)
                    {
                        report.Add(repos.GenerateReport().report[i]);
                    }
                }
            }
            
        }

        public void GenerateInString()
        {
            foreach (var t in report)
            {
                Console.WriteLine($"Имя задачи: {t.ResolvedTask.TaskName}, Дата изменения: {t.LastEditTime}, изменения внёс {t.ResolvedTask.Responsible.Name} ");
            }
        }
      
    }
}
