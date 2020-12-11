using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Task.SearchTask;

namespace ReportingSystem.Task
{
   public interface ITaskSystem
   {
       public IManageTask Create(string Name, Description description);

       public IManageTask ChangeStatus(Task task, TaskStatus status);

       public IManageTask ChangeWorker(Task task, Worker.Worker worker);

       public IManageTask AddComment(Task task, string comment);

       public ISearchAlgorithms SearchByID();


   }
}
