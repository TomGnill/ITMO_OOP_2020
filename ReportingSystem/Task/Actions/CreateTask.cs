using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
    public class CreateTask : IManageTask
    {
        public Task NewTask;

        public CreateTask(string Name, Description description)
        {
            NewTask = new Task(Name, description);
            Act();
        }

        public Task Act()
        {
            NewTask.Status = TaskStatus.Open;
            return NewTask;
        }
    }
}
