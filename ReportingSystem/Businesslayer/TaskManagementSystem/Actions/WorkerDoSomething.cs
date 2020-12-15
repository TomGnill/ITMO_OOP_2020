using ReportingSystem.DataAccesslayer.Task;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.Actions
{
   public class WorkerDoSomething : IManageTask
   {
       public DataAccesslayer.Task.Task OurTask;


       public WorkerDoSomething(DataAccesslayer.Task.Task task)
       {
           OurTask = task;
           SomeActionInTask();
       }

       public DataAccesslayer.Task.Task SomeActionInTask()
       {
           OurTask.Status = TaskStatus.Active;
           return OurTask;
       }
   }
}
