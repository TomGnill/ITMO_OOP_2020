using System.Collections.Generic;
using System.Linq;
using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.SearchTask
{
   public class ExecuteSearchByWorker : IExecuteSearch
   {
       
       public List<Worker> Workers;
       public List<DataAccesslayer.Task.Task> TaskList;
       public Dictionary<DataAccesslayer.Task.Task, uint> AllTasks;

       public ExecuteSearchByWorker(Worker boss, Dictionary<DataAccesslayer.Task.Task, uint> list)
       {
           AllTasks = list;
           Workers = boss.SubWorkers;
           ReturnTasks();
       }

       public List<DataAccesslayer.Task.Task> ReturnTasks()
       {
           TaskList = new List<DataAccesslayer.Task.Task>();
           foreach (var task in from task in AllTasks from worker in Workers where task.Key.Responsible == worker select task)
           {
               TaskList.Add(task.Key);
           }

           ReturnTasks();
           return TaskList;
       }
   }
}
