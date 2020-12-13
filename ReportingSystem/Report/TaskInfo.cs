using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report
{
   public class TaskInfo //Слой данных
   {
        public Task.Task ResolvedTask;
        public DateTime CreateTime;
        public DateTime LastEditTime;
        public Change Something;
        public Worker.Worker WhoDoAction;
        public string Comment;

        public TaskInfo(Task.Task task, DateTime time)
        {
            ResolvedTask = task;
            CreateTime = time;
            LastEditTime = time;
            Something = Change.Creating;
        }

        public enum Change
        {
            Creating,
            ChangeWorker,
            AddComment,
            ChangeStatus,
            WorkerDoSomething
        }
   }
}
