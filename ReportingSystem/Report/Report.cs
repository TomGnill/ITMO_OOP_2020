using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportingSystem.Report.Actions;

namespace ReportingSystem.Report
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
                if (repos.ReturnReport().report != null)
                {
                    for (int i = 0; i < repos.ReturnReport().report.Count; i++)
                    {
                        report.Add(repos.ReturnReport().report[i]);
                    }
                }
            }
            
        }
    }
}
