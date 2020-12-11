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
            Act();
        }

        public Task Act()
        {
            SomeTask.Comment.Add(SomeComment);
            return SomeTask;
        }
    }
}
