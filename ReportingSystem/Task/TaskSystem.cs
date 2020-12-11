using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Task.Actions;
using ReportingSystem.Worker;

namespace ReportingSystem.Task
{
   public class TaskSystem : ITaskSystem  // Слой данных
   {
       public Dictionary<Task, uint> TaskList;

       public TaskSystem()
       {
           TaskList = new Dictionary<Task, uint>();
       }

       public IManageTask Create(string Name, Description description)
       {
           Task newTask = new Task(Name, description);

           TaskList.Add(newTask, GiveId());

           return new CreateTask(Name, description);
       }

       public IManageTask AddComment(Task task, string comment)
       {
           return new AddTaskComment(task,comment);
       }

       public IManageTask ChangeStatus(Task task, TaskStatus newStatus)
       {
           return new ChangeTaskStatus(task, newStatus);
       }

       public IManageTask ChangeWorker(Task task, Worker.Worker worker)
       {
           return new CnangeTaskWorker(task, worker);
       }

       private uint GiveId()
       {
           uint id = 0;

           if (TaskList.Count == 0)
           {
               id = 1;
               return id;
           }
           foreach (var pair in TaskList)
           {
                   id = pair.Value;
           }
           id += 1;

               return id;
       }
   }
}
