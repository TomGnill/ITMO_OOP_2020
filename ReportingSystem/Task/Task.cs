using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task
{
    public class Task // Слой данных
    {
        public string TaskName;
        public Description Description;
        public Worker.Worker Responsible;
        public TaskStatus Status;
        public string Comment;

        public Task(string taskName, Description description)
        {
            TaskName = taskName;
            Description = description;
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
