using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportingSystem.Report
{
    class Report// Слой данных
    {
        public List<TaskInfo> report;
        public Report()// за день
        {
            report = new List<TaskInfo>();
        }

        public Report(List<Report> reports)//спринт
        {
            report = new List<TaskInfo>();
            for (int i = 0; i < reports.Count; i++)
            {
                report.Union(reports[i].report);
            }
        }
    }
}
