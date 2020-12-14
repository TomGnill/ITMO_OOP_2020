using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Report;
using ReportingSystem.Report.Actions;
using ReportingSystem.Task.Actions;
using ReportingSystem.Task.SearchTask;
using ReportingSystem.Worker;

namespace ReportingSystem.Task
{
    public class TaskSystem : ITaskSystem // Слой данных
    {
        public Dictionary<Task, uint> TaskList;
        public List<TaskInfo> Log;
        public List<Worker.Worker> Workers;

        public TaskSystem()
        {
            Workers = new List<Worker.Worker>();
            TaskList = new Dictionary<Task, uint>();
            Log = new List<TaskInfo>();
        }

        public IManageTask Create(string Name, Description description)
        {
            Task newTask = new Task(Name, description);

            TaskInfo newInfo = new TaskInfo(newTask, DateTime.Now.Date);
            FixChange(newTask, TaskInfo.Change.Creating);
            TaskList.Add(newTask, GiveId());

            return new CreateTask(Name, description);
        }

        public IManageTask AddComment(Task task, string comment)
        {

            FixComment(task, comment);

            return new AddTaskComment(task, comment);
        }

        public IManageTask ChangeStatus(Task task, TaskStatus newStatus)
        {
            FixChange(task, TaskInfo.Change.ChangeStatus);

            return new ChangeTaskStatus(task, newStatus);
        }

        public IManageTask ChangeWorker(Task task, Worker.Worker worker)
        {
            FixChange(task, TaskInfo.Change.ChangeWorker);

            return new CnangeTaskWorker(task, worker);
        }

        public IManageTask WorkerDoSomething(Task task)
        {
            FixChange(task, TaskInfo.Change.WorkerDoSomething);

            return new WorkerDoSomething(task);
        }

        public ISearchAlgorithms SearchByID(uint id)
        {
            return new SearchByTaskID(TaskList, id);
        }

        public ISearchAlgorithms SearchByLastEditDate(DateTime time)
        {
            return new SearchByDate(Log, SearchByDate.Mode.LastEditing);
        }

        public ISearchAlgorithms SearchByCreateDate(DateTime time)
        {
            return new SearchByDate(Log, SearchByDate.Mode.CreatingDate);
        }

        public ISearchAlgorithms SearchByWorker(Worker.Worker Boss)
        {
            return new SearchByWorker(Boss, TaskList);
        }

        public ISearchAlgorithms SearchByEdit(Worker.Worker worker)
        {
            return new SearchByEditing(worker, Log);
        }

        public IFormReport FormDayReport(Worker.Worker worker, DateTime date)
        {
            return new FormDayReport(worker, Log, date);
        }

        public IFormReport FormBossReport(Worker.Worker worker, DateTime date)
        {
            return new FormBossReport(worker, date, Log);
        }

        public IFormReport FormFinalReport(Worker.Worker worker, DateTime date)
        {
            return new FormFinalReport(worker, date, Log);
        }

        public IManageWorkers GiveChief(string ChiefName, string WorkerName)
        {
            return new ChangeChief(Workers, ChiefName, WorkerName);
        }

        public IManageWorkers GiveWorkers(string ChiefName, List<Worker.Worker> workers)
        {
            return new GiveWorkers(workers, Workers, ChiefName);
        }

        public void FixChange(Task task, TaskInfo.Change someChange)
        {
            TaskInfo newLine = new TaskInfo(task, DateTime.Now);
            newLine.LastEditTime = DateTime.Now.Date;
            newLine.Something = someChange;
            switch (newLine.Something)
            {
                case TaskInfo.Change.WorkerDoSomething:
                    newLine.WhoDoAction = task.Responsible;
                    break;
                case TaskInfo.Change.ChangeStatus:
                {
                    if (newLine.ResolvedTask.Status == TaskStatus.Resolved)
                    {
                        newLine.WhoDoAction = task.Responsible.Chief;
                    }

                    break;
                }
            }

            Log.Add(newLine);
        }


        public void FixComment(Task task, string comment)
        {

            var newLine = new TaskInfo(task, DateTime.Now);
            newLine.LastEditTime = DateTime.Now.Date;
            newLine.Something = TaskInfo.Change.AddComment;
            newLine.Comment = comment;
            Log.Add(newLine);

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

        public Worker.Worker CreateWorker(string name)
        {
            Worker.Worker newWorker = new Worker.Worker(name);
            Workers.Add(newWorker);
            return newWorker;
        }

   }
}
