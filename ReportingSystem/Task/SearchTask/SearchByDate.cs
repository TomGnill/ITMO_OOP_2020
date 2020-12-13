using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Report;

namespace ReportingSystem.Task.SearchTask
{
   public class SearchByDate : ISearchAlgorithms
   {
       public DateTime Time;
       public List<Task> TaskList;
       public List<TaskInfo> TaskInfos;
       public Mode SomeMode;


       public SearchByDate(List<TaskInfo> info, Mode ourmode)
       {
          TaskInfos = info;
          SomeMode = ourmode;
       }

       public void SearchByCreatingDate(DateTime time)
       {
           TaskList = new List<Task>();
           foreach (var task in TaskInfos)
           {
               if (task.CreateTime == Time)
               {
                   TaskList.Add(task.ResolvedTask);
               }
           }
       }

       public void SearchByLastEditingDate(DateTime time)
       {
           TaskList = new List<Task>();
           foreach (var task in TaskInfos)
           {
               if (task.LastEditTime == Time)
               {
                   TaskList.Add(task.ResolvedTask);
               }
           }
       }

       public List<Task> ReturnTasks()
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
