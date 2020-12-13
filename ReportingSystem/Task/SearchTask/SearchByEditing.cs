using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Report;

namespace ReportingSystem.Task.SearchTask
{
    public class SearchByEditing : ISearchAlgorithms
    {
        public Worker.Worker Worker;
        public List<TaskInfo> Log;
        public List<Task> Tasks;

        public SearchByEditing(Worker.Worker worker, List<TaskInfo> log)
        {
            Worker = worker;
            Log = log;
            Algorytm();
        }

        public void Algorytm()
        {
            Tasks = new List<Task>();
            foreach (var taskInfo in Log)
            {
                if (taskInfo.WhoDoAction == Worker)
                {
                    Tasks.Add(taskInfo.ResolvedTask);
                }
            }
        }

        public List<Task> ReturnTasks()
        {
            return Tasks;
        }
    }
}
