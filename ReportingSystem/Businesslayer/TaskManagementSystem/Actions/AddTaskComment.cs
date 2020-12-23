namespace ReportingSystem.Businesslayer.TaskManagementSystem.Actions
{
    public class AddTaskComment : IManageTask
    {
        public DataAccesslayer.Task.Task SomeTask;
        public string SomeComment;

        public AddTaskComment(DataAccesslayer.Task.Task task, string comment)
        {
            SomeTask = task;
            SomeComment = comment;
            SomeActionInTask();
        }

        public DataAccesslayer.Task.Task SomeActionInTask()
        {
            SomeTask.Comment.Add(SomeComment);
            return SomeTask;
        }
    }
}
