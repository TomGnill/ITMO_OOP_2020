using System;
using System.Collections.Generic;
using System.Linq;
using ReportingSystem.DataAccesslayer.Report;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.SearchTask
{
   public class ExecuteSearchByDate : IExecuteSearch
   {
       public DateTime Time;
       public List<DataAccesslayer.Task.Task> TaskList;
       public List<TaskInfo> TaskInfos;
       public Mode SomeMode;


       public ExecuteSearchByDate(List<TaskInfo> info, Mode ourMode)
       {
          TaskInfos = info;
          SomeMode = ourMode;
       }

       public void SearchByCreatingDate(DateTime time)
       {
           TaskList = new List<DataAccesslayer.Task.Task>();
           foreach (var task in TaskInfos.Where(task => task.CreateTime.Date == Time.Date))
           {
               TaskList.Add(task.ResolvedTask);
           }
       }

       public void SearchByLastEditingDate(DateTime time)
       {
           TaskList = new List<DataAccesslayer.Task.Task>();
           foreach (var task in TaskInfos.Where(task => task.LastEditTime.Date == Time.Date))
           {
               TaskList.Add(task.ResolvedTask);
           }
       }

       public List<DataAccesslayer.Task.Task> ReturnTasks()
       {
           if (SomeMode == Mode.CreatingDate)
           {
                SearchByCreatingDate(Time);
           }

           if (SomeMode == Mode.LastEditing)
           {
               SearchByLastEditingDate(Time);
           }
           return TaskList;
       }

       public enum Mode
       {
           CreatingDate,
           LastEditing
       }
   }
}
