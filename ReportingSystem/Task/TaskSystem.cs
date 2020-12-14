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
    public class TaskSystem : ITaskSystem // Слой данных```
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
            var newTask = new Task(Name, description);

            
            Log.Add(new TaskInfo(newTask, DateTime.Now));

            TaskList.Add(newTask, Convert.ToUInt32(TaskList.Count + 1));

            return new CreateTask(Name, description);
        }

        public IManageTask AddComment(Task task, string comment)
        {

            RecordCommentInLog(task, comment);

            return new AddTaskComment(task, comment);
        }

        public IManageTask ChangeStatus(Task task, TaskStatus newStatus)
        {
            RecordChange(task, TaskInfo.Option.ChangeStatus);

            return new ChangeTaskStatus(task, newStatus);
        }

        public IManageTask ChangeWorker(Task task, Worker.Worker worker)
        {
            RecordChange(task, TaskInfo.Option.ChangeWorker);

            return new ChangeTaskWorker(task, worker);
        }

        public IManageTask WorkerDoSomething(Task task)
        {
            RecordChange(task, TaskInfo.Option.WorkerDoSomething);

            return new WorkerDoSomething(task);
        }

        public IExecuteSearch SearchById(uint id)
        {
            return new ExecuteSearchByTaskId(TaskList, id);
        }

        public IExecuteSearch SearchByLastEditDate(DateTime time)
        {
            return new ExecuteSearchByDate(Log, ExecuteSearchByDate.Mode.LastEditing);
        }

        public IExecuteSearch SearchByCreateDate(DateTime time)
        {
            return new ExecuteSearchByDate(Log, ExecuteSearchByDate.Mode.CreatingDate);
        }

        public IExecuteSearch SearchByWorker(Worker.Worker Boss)
        {
            return new ExecuteSearchByWorker(Boss, TaskList);
        }

        public IExecuteSearch SearchByEdit(Worker.Worker worker)
        {
            return new ExecuteSearchByEditing(worker, Log);
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

        public void RecordChange(Task task, TaskInfo.Option someOption)
        {
            switch (someOption)
            {
                case TaskInfo.Option.WorkerDoSomething:
                   Log.Add(new TaskInfo(task, DateTime.Now, task.Responsible));
                    break;
                case TaskInfo.Option.ChangeStatus:
                {
                    if (task.Status == TaskStatus.Resolved)
                    {
                        Log.Add(new TaskInfo(task, DateTime.Now, task.Responsible.Chief, TaskInfo.Option.ChangeStatus));break;
                    }
                    
                    Log.Add(new TaskInfo(task, DateTime.Now, task.Responsible, TaskInfo.Option.ChangeStatus));break;
                }
                case TaskInfo.Option.ChangeWorker:
                    Log.Add(new TaskInfo(task, DateTime.Now, task.Responsible, TaskInfo.Option.ChangeWorker));
                    break;

            }
        }

        public void RecordCommentInLog(Task task, string comment)
        {
            var newLine = new TaskInfo(task, DateTime.Now, comment );
            Log.Add(newLine);
        }

        public Worker.Worker CreateWorker(string name)
        {
            Worker.Worker newWorker = new Worker.Worker(name);
            Workers.Add(newWorker);
            return newWorker;
        }

   }
}
