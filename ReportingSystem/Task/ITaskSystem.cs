using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Task.SearchTask;

namespace ReportingSystem.Task
{
   public interface ITaskSystem
   {
       public IManageTask Create(string Name, Description description);

       public IManageTask ChangeStatus(Task task, TaskStatus status);

       public IManageTask ChangeWorker(Task task, Worker.Worker worker);

       public IManageTask AddComment(Task task, string comment);

       public IManageTask WorkerDoSomething(Task task);

       public ISearchAlgorithms SearchByID(uint id);

       public ISearchAlgorithms SearchByLastEditDate(DateTime time);

       public ISearchAlgorithms SearchByCreateDate(DateTime time);

       public ISearchAlgorithms SearchByWorker(Worker.Worker Boss);

       public ISearchAlgorithms SearchByEdit(Worker.Worker worker);





    }
}
