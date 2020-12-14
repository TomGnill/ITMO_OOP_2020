using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportingSystem.Report;

namespace ReportingSystem.Task.SearchTask
{
    public class ExecuteSearchByEditing : IExecuteSearch
    {
        public Worker.Worker Worker;
        public List<TaskInfo> Log;
        public List<Task> Tasks;

        public ExecuteSearchByEditing(Worker.Worker worker, List<TaskInfo> log)
        {
            Worker = worker;
            Log = log;
            ReturnTasks();
        }

        public List<Task> ReturnTasks()
        { 
            Tasks = new List<Task>();
            foreach (var taskInfo in Log.Where(taskInfo => taskInfo.Person == Worker))
            {
                Tasks.Add(taskInfo.ResolvedTask);
            }
            return Tasks;
        }
    }
}
