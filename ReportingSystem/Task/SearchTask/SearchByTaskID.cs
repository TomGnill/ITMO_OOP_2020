using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.SearchTask
{
   public class SearchByTaskID : ISearchAlgorithms
   {
       public Task FindTask;
       public Dictionary<Task, uint> TaskList;
       public uint ID;

       public SearchByTaskID( Dictionary<Task, uint> taskList, uint id)
       {
           TaskList = taskList;
           ID = id;
           Search();
       }

       public void Search()
       {
           foreach (var task in TaskList)
           {
               if (task.Value == ID)
               {
                   FindTask = task.Key;
                   ReturnTasks();
               }
           }
       }
       public List<Task> ReturnTasks()
       {
           List<Task> newList = new List<Task>{FindTask};
           return newList;
       }
   }
}
