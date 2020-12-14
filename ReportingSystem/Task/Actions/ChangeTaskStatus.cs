using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
    public class ChangeTaskStatus : IManageTask
    {
        public Task SomeTask;
        public TaskStatus NewStatus;
        public Worker.Worker WhoDoAction;

        public ChangeTaskStatus(Task ourTask, TaskStatus taskStatus)
        {
            SomeTask = ourTask;
            NewStatus = taskStatus;
            SomeActionInTask();
        }

        public Task SomeActionInTask()
        { 
            if (SomeTask.Status != TaskStatus.Resolved)
            {
                SomeTask.Status = NewStatus;
            }
            return SomeTask;
        }
    }
}
