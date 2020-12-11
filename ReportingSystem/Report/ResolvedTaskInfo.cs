using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report
{
    class ResolvedTaskInfo //Слой данных
    {
        public Task.Task ResolvedTask;
        public DateTime CompTime;

        public ResolvedTaskInfo(Task.Task task, DateTime time)
        {
            ResolvedTask = task;
            CompTime = time;
        }
    }
}
