using System;
using System.Collections.Generic;
using ReportingSystem.Businesslayer.FormReport;
using ReportingSystem.Businesslayer.TaskManagementSystem.Actions;
using ReportingSystem.Businesslayer.TaskManagementSystem.SearchTask;
using ReportingSystem.Businesslayer.WorkersManagement;
using ReportingSystem.DataAccesslayer.Task;
using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Task
{
   public interface ITaskSystem
   {
       public IManageTask Create(string Name, Description description);

       public IManageTask ChangeStatus(DataAccesslayer.Task.Task task, TaskStatus status);

       public IManageTask ChangeWorker(DataAccesslayer.Task.Task task, Worker worker);

       public IManageTask AddComment(DataAccesslayer.Task.Task task, string comment);

       public IManageTask WorkerDoSomething(DataAccesslayer.Task.Task task);

       public IExecuteSearch SearchById(uint id);

       public IExecuteSearch SearchByLastEditDate(DateTime time);

       public IExecuteSearch SearchByCreateDate(DateTime time);

       public IExecuteSearch SearchByWorker(Worker Boss);

       public IExecuteSearch SearchByEdit(Worker worker);

       public IFormReport FormDayReport(Worker worker, DateTime date);

       public IFormReport FormBossReport(Worker worker, DateTime date);

       public IFormReport FormFinalReport(Worker worker, DateTime date);

       public IManageWorkers GiveChief(string ChiefName, string WorkerName);

       public IManageWorkers GiveWorkers(string ChiefName, List<Worker> workers);
   }
}
