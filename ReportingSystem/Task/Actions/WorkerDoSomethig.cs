using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
   public class WorkerDoSomethig : IManageTask
   {
       public Task OurTask;


       public WorkerDoSomethig(Task task)
       {
           OurTask = task;
       }

       public Task Act()
       {
           OurTask.Status = TaskStatus.Active;
           return OurTask;
       }
   }
}
