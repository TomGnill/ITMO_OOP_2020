using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
    public class AddTaskComment : IManageTask
    {
        public Task SomeTask;
        public string SomeComment;

        public AddTaskComment(Task task, string comment)
        {
            SomeTask = task;
            SomeComment = comment;
            SomeActionInTask();
        }

        public Task SomeActionInTask()
        {
            SomeTask.Comment.Add(SomeComment);
            return SomeTask;
        }
    }
}
