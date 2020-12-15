using System.Collections.Generic;
using System.Linq;
using ReportingSystem.DataAccesslayer.Report;
using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.SearchTask
{
    public class ExecuteSearchByEditing : IExecuteSearch
    {
        public Worker Worker;
        public List<TaskInfo> Log;
        public List<DataAccesslayer.Task.Task> Tasks;

        public ExecuteSearchByEditing(Worker worker, List<TaskInfo> log)
        {
            Worker = worker;
            Log = log;
            ReturnTasks();
        }

        public List<DataAccesslayer.Task.Task> ReturnTasks()
        { 
            Tasks = new List<DataAccesslayer.Task.Task>();
            foreach (var taskInfo in Log.Where(taskInfo => taskInfo.Person == Worker))
            {
                Tasks.Add(taskInfo.ResolvedTask);
            }
            return Tasks;
        }
    }
}
