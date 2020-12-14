using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report
{
   public class TaskInfo //Слой данных
   {
       public Task.Task ResolvedTask { get; }
        public DateTime CreateTime { get; }
        public DateTime LastEditTime { get; }
        public Option Something { get; }
        public Worker.Worker Person { get; }
        public string Comment { get; }

        public TaskInfo(Task.Task task, DateTime time)
        {
            ResolvedTask = task;
            CreateTime = time;
            LastEditTime = time;
            Something = Option.Creating;
        }

        public TaskInfo(Task.Task task, DateTime time, string comment)
        {
            ResolvedTask = task;
            LastEditTime = time;
            Comment = comment;
            Something = Option.AddComment;
        }

        public TaskInfo(Task.Task task, DateTime time, Worker.Worker person)
        {
            ResolvedTask = task;
            LastEditTime = time;
            Person = person;
            Something = Option.WorkerDoSomething;
        }

        public TaskInfo(Task.Task task, DateTime time, Worker.Worker person, Option someoption)
        {
            ResolvedTask = task;
            LastEditTime = time;
            Person = person;
            Something = someoption;
        }

        public enum Option
        {
            Creating,
            ChangeWorker,
            AddComment,
            ChangeStatus,
            WorkerDoSomething
        }
   }
}
