using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.SearchTask
{
   public class SearchByWorker : ISearchAlgorithms
   {
       
       public List<Worker.Worker> Workers;
       public List<Task> TaskList;
       public Dictionary<Task, uint> AllTasks;

       public SearchByWorker(Worker.Worker boss, Dictionary<Task, uint> list)
       {
           AllTasks = list;
           Workers = boss.SubWorkers;
           Algorythm();
       }

       public void Algorythm()
       {
           TaskList = new List<Task>();
           foreach (var task in AllTasks)
           {
               foreach (var worker in Workers)
               {
                   if (task.Key.Responsible == worker)
                   {
                       TaskList.Add(task.Key);
                   }
               }
           }

           ReturnTasks();
       }

       public List<Task> ReturnTasks()
       {
           return TaskList;
       }
   }
}
