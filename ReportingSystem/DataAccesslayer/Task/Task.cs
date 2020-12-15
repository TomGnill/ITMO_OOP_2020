using System.Collections.Generic;

namespace ReportingSystem.DataAccesslayer.Task
{
    public class Task // Слой данных
    {
        public string TaskName;
        public Description Description;
        public Worker.Worker Responsible;
        public TaskStatus Status;
        public List<string> Comment;

        public Task(string taskName, Description description)
        {
            TaskName = taskName;
            Description = description;
            Comment = new List<string>();
        }
    }

    public class Description// Слой данных
    {
        public string SomeText;

        public Description(string text)
        {
            SomeText = text;
        }
    }

    public enum TaskStatus// Слой данных
    {
        Open,
        Active,
        Resolved
    }
}
