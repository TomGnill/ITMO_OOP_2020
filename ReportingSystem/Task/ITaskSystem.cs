﻿using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Report.Actions;
using ReportingSystem.Task.SearchTask;
using ReportingSystem.Worker;

namespace ReportingSystem.Task
{
   public interface ITaskSystem
   {
       public IManageTask Create(string Name, Description description);

       public IManageTask ChangeStatus(Task task, TaskStatus status);

       public IManageTask ChangeWorker(Task task, Worker.Worker worker);

       public IManageTask AddComment(Task task, string comment);

       public IManageTask WorkerDoSomething(Task task);

       public IExecuteSearch SearchById(uint id);

       public IExecuteSearch SearchByLastEditDate(DateTime time);

       public IExecuteSearch SearchByCreateDate(DateTime time);

       public IExecuteSearch SearchByWorker(Worker.Worker Boss);

       public IExecuteSearch SearchByEdit(Worker.Worker worker);

       public IFormReport FormDayReport(Worker.Worker worker, DateTime date);

       public IFormReport FormBossReport(Worker.Worker worker, DateTime date);

       public IFormReport FormFinalReport(Worker.Worker worker, DateTime date);

       public IManageWorkers GiveChief(string ChiefName, string WorkerName);

       public IManageWorkers GiveWorkers(string ChiefName, List<Worker.Worker> workers);
   }
}
