using System.Collections.Generic;
using System.Linq;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.SearchTask
{
   public class ExecuteSearchByTaskId : IExecuteSearch
   {
       public DataAccesslayer.Task.Task FindTask;
       public Dictionary<DataAccesslayer.Task.Task, uint> TaskList;
       public uint Id;

       public ExecuteSearchByTaskId( Dictionary<DataAccesslayer.Task.Task, uint> taskList, uint id)
       {
           TaskList = taskList;
           Id = id;
           ReturnTasks();
       }
       public List<DataAccesslayer.Task.Task> ReturnTasks()
       {
           foreach (var task in TaskList.Where(task => task.Value == Id))
           {
               FindTask = task.Key;
               ReturnTasks();
           }
           var newList = new List<DataAccesslayer.Task.Task>{FindTask};
           return newList;
       }
   }
}
