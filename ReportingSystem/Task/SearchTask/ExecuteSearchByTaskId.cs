using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportingSystem.Task.SearchTask
{
   public class ExecuteSearchByTaskId : IExecuteSearch
   {
       public Task FindTask;
       public Dictionary<Task, uint> TaskList;
       public uint Id;

       public ExecuteSearchByTaskId( Dictionary<Task, uint> taskList, uint id)
       {
           TaskList = taskList;
           Id = id;
           ReturnTasks();
       }
       public List<Task> ReturnTasks()
       {
           foreach (var task in TaskList.Where(task => task.Value == Id))
           {
               FindTask = task.Key;
               ReturnTasks();
           }
           var newList = new List<Task>{FindTask};
           return newList;
       }
   }
}
