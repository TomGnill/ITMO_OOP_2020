using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report
{
    class TaskInfo //Слой данных
    {
        public Task.Task ResolvedTask;
        public DateTime CreatTime;
        public DateTime LastEditTime;

        public TaskInfo(Task.Task task, DateTime time)
        {
            ResolvedTask = task;
            CreatTime = time;
            LastEditTime = time;
        }
    }
}
