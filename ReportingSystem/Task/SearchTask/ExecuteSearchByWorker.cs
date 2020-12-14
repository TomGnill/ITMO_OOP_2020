using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportingSystem.Task.SearchTask
{
   public class ExecuteSearchByWorker : IExecuteSearch
   {
       
       public List<Worker.Worker> Workers;
       public List<Task> TaskList;
       public Dictionary<Task, uint> AllTasks;

       public ExecuteSearchByWorker(Worker.Worker boss, Dictionary<Task, uint> list)
       {
           AllTasks = list;
           Workers = boss.SubWorkers;
           ReturnTasks();
       }

       public List<Task> ReturnTasks()
       {
           TaskList = new List<Task>();
           foreach (var task in from task in AllTasks from worker in Workers where task.Key.Responsible == worker select task)
           {
               TaskList.Add(task.Key);
           }

           ReturnTasks();
           return TaskList;
       }
   }
}
