using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
   public class WorkerDoSomething : IManageTask
   {
       public Task OurTask;


       public WorkerDoSomething(Task task)
       {
           OurTask = task;
           SomeActionInTask();
       }

       public Task SomeActionInTask()
       {
           OurTask.Status = TaskStatus.Active;
           return OurTask;
       }
   }
}
