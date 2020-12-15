using System;
using System.Collections.Generic;
using ReportingSystem.Businesslayer.FormReport;
using ReportingSystem.Businesslayer.TaskManagementSystem.Actions;
using ReportingSystem.Businesslayer.TaskManagementSystem.SearchTask;
using ReportingSystem.Businesslayer.WorkersManagement;
using ReportingSystem.DataAccesslayer.Report;
using ReportingSystem.DataAccesslayer.Task;
using ReportingSystem.DataAccesslayer.Worker;
using ReportingSystem.Task;

namespace ReportingSystem.Businesslayer.TaskManagementSystem
{
    public class TaskSystem : ITaskSystem // Слой данных```
    {
        public Dictionary<DataAccesslayer.Task.Task, uint> TaskList;
        public List<TaskInfo> Log;

        public TaskSystem()
        {
            TaskList = new Dictionary<DataAccesslayer.Task.Task, uint>();
            Log = new List<TaskInfo>();
        }

        public IManageTask Create(string Name, Description description)
        {
            var newTask = new DataAccesslayer.Task.Task(Name, description);

            
            Log.Add(new TaskInfo(newTask, DateTime.Now));

            TaskList.Add(newTask, Convert.ToUInt32(TaskList.Count + 1));

            return new CreateTask(Name, description);
        }

        public IManageTask AddComment(DataAccesslayer.Task.Task task, string comment)
        {

            RecordCommentInLog(task, comment);

            return new AddTaskComment(task, comment);
        }

        public IManageTask ChangeStatus(DataAccesslayer.Task.Task task, TaskStatus newStatus)
        {
            RecordChange(task, TaskInfo.Option.ChangeStatus);
            RecordCommentInLog(task, $"Состояние задачи изменилось на {task.Status}");

            return new ChangeTaskStatus(task, newStatus);
        }

        public IManageTask ChangeWorker(DataAccesslayer.Task.Task task, Worker worker)
        {
            RecordChange(task, TaskInfo.Option.ChangeWorker);

            return new ChangeTaskWorker(task, worker);
        }

        public IManageTask WorkerDoSomething(DataAccesslayer.Task.Task task)
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

        public IExecuteSearch SearchByWorker(Worker Boss)
        {
            return new ExecuteSearchByWorker(Boss, TaskList);
        }

        public IExecuteSearch SearchByEdit(Worker worker)
        {
            return new ExecuteSearchByEditing(worker, Log);
        }

        public IFormReport FormDayReport(Worker worker, DateTime date)
        {
            return new FormDayReport(worker, Log, date);
        }

        public IFormReport FormBossReport(Worker worker, DateTime date)
        {
            return new FormBossReport(worker, date, Log);
        }

        public IFormReport FormFinalReport(Worker worker, DateTime date)
        {
            return new FormFinalReport(worker, date, Log);
        }

        public IManageWorkers GiveChief(string ChiefName, string WorkerName)
        {
            return new ChangeChief(ChiefName, WorkerName);
        }

        public IManageWorkers GiveWorkers(string ChiefName, List<Worker> workers)
        {
            return new GiveWorkers(workers, ChiefName);
        }

        public void RecordChange(DataAccesslayer.Task.Task task, TaskInfo.Option someOption)
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

        public void RecordCommentInLog(DataAccesslayer.Task.Task task, string comment)
        {
            var newLine = new TaskInfo(task, DateTime.Now, comment );
            Log.Add(newLine);
        }

        public Worker CreateWorker(string name)
        { 
            Worker newWorker = new Worker{Name = name, Chief = null, SubWorkers = new List<Worker>()};
            using (WorkerContext db = new WorkerContext())
            {
                db.Workers.Add(newWorker);
                db.SaveChanges();
            }
            return newWorker;
        }

   }
}
