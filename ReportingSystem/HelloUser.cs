using System;
using System.Collections.Generic;
using System.Text;
using ReportingSystem.Report;
using ReportingSystem.Task;

namespace ReportingSystem
{
    public class HelloUser // пользовательский слой
    {
        public ITaskSystem System;
        public TaskSystem newSystem;

        public HelloUser()
        {
            newSystem = new TaskSystem();
            System = newSystem;
        }
        public void PrintReport(ReportMode mode, Worker.Worker worker, DateTime date)
        {
            List<TaskInfo> reports = new List<TaskInfo>();
            switch (mode)
            {
                case ReportMode.DayReport:
                    reports =  System.FormDayReport(worker, date).ReturnReport().report;
                    break;
                case ReportMode.BossReport:
                    reports = System.FormBossReport(worker, date).ReturnReport().report;
                    break;
                case ReportMode.FinalReport:
                    reports = System.FormFinalReport(worker, date).ReturnReport().report;
                    break;
            }

            if(reports.Count != 0)
             for (int i = 0; i < reports.Count; i++)
             {
                    Console.WriteLine($"Имя задачи: {reports[i].ResolvedTask.TaskName}, Дата изменения: {reports[i].LastEditTime}, изменения внёс {reports[i].ResolvedTask.Responsible.Name} ");
              
             }
        }

        public void PrintLog()
        {
            foreach (var taskInfo in newSystem.Log)
            {
                if (taskInfo.Something == TaskInfo.Change.WorkerDoSomething)
                {
                    Console.WriteLine($"Работяга {taskInfo.ResolvedTask.Responsible.Name} внёс изменения в задачу {taskInfo.ResolvedTask.TaskName} ({taskInfo.LastEditTime})");
                }
                if (taskInfo.Something == TaskInfo.Change.ChangeWorker)
                {
                    Console.WriteLine($"У задачи {taskInfo.ResolvedTask.TaskName}, сменился исполнитель на {taskInfo.ResolvedTask.Responsible.Name}");
                }
                if (taskInfo.Something == TaskInfo.Change.Creating)
                {
                    Console.WriteLine($"Создана задача {taskInfo.ResolvedTask.TaskName} {taskInfo.CreateTime.Date}");
                }

                if (taskInfo.Something == TaskInfo.Change.AddComment)
                {
                    Console.WriteLine($"пользователь {taskInfo.ResolvedTask.Responsible.Name} прокомментировал задачу {taskInfo.ResolvedTask.TaskName}: {taskInfo.Comment}");
                }
                if (taskInfo.Something == TaskInfo.Change.ChangeStatus)
                {
                    Console.WriteLine($"пользоватенль {taskInfo.ResolvedTask.Responsible.Chief.Name} пометил задачу {taskInfo.ResolvedTask.TaskName} как {taskInfo.ResolvedTask.Status}");
                }
                
            }
        }

        public void SearchByID(uint id)
        {
           List<Task.Task> newList = System.SearchByID(id).ReturnTasks();
           foreach (var task in newList)
           {
               Console.WriteLine($"Имя задачи {task.TaskName}");
           }
        }

        public void SearchByEdit(string name)
        {
            foreach (var worker in newSystem.Workers)
            {
                if (worker.Name == name)
                {
                    List<Task.Task> newList = System.SearchByEdit(worker).ReturnTasks();
                    foreach (var task in newList)
                    {
                       Console.WriteLine($"Имя задачи {task.TaskName}");
                    }
                }
            }
        }

        public void SearchByWorker(string Name)
        {
            foreach (var worker in newSystem.Workers)
            {
                if (worker.Name == Name)
                {
                    List<Task.Task> newList = System.SearchByWorker(worker).ReturnTasks();
                    foreach (var task in newList)
                    {
                        Console.WriteLine($"Имя задачи {task.TaskName}");
                    }
                }
            }
        }

        public void SearchByCreatingDate(DateTime date)
        {
            List<Task.Task> newList = System.SearchByCreateDate(date).ReturnTasks();
            foreach (var task in newList)
            {
                Console.WriteLine($"Имя задачи {task.TaskName}");
            }
        }

        public void SearchLastEditingDate(DateTime date)
        {
            List<Task.Task> newList = System.SearchByLastEditDate(date).ReturnTasks();
            foreach (var task in newList)
            {
                Console.WriteLine($"Имя задачи {task.TaskName}");
            }
        }

        public void ChangesInCode(uint id)
        {
            foreach (var task in newSystem.TaskList)
            {
                if (task.Value == id)
                {
                    System.WorkerDoSomething(task.Key);
                }
            }
            Console.WriteLine("работяга внёс изменения в задачу");
        }

        public void CreateTask(string TaskName, string Comment)
        {
            Description comment= new Description(Comment);
            System.Create(TaskName, comment);
            Console.WriteLine("Создана новая задача");
        }

        public void WriteComment(uint id, string comment)
        {
            foreach (var task in newSystem.TaskList)
            {
                if (task.Value == id)
                {
                    System.AddComment(task.Key, comment);
                }
            }
            Console.WriteLine("Задача прокомментирована");
        }

        public void ChangeWorker(uint id, string WorkerName)
        {
            Worker.Worker Worker = null;
            foreach (var worker in newSystem.Workers)
            {
                if (worker.Name == WorkerName)
                {
                    Worker = worker;
                }

            }

            foreach (var task in newSystem.TaskList)
            {
                if (task.Value == id)
                {
                    System.ChangeWorker(task.Key, Worker);
                }
            }
        }

        public void ChangeTaskStatus(uint id, TaskStatus status)
        {
            foreach (var task in newSystem.TaskList)
            {
                if (task.Value == id)
                {
                    System.ChangeStatus(task.Key, status);
                }
            }

        }

        public Worker.Worker CreateWorker(string Name)
        {
            var worker = newSystem.CreateWorker(Name);
            Console.WriteLine("Работяга создан");
            return worker;
        }

        public void GiveWorkeres(string ChiefName, List<Worker.Worker> workers)
        {
            System.GiveWorkers(ChiefName, workers);
            Console.WriteLine("Работяги переданы");
        }

        public void ChangeChief(string ChiefName, string WorkerName)
        {
            System.GiveChief(ChiefName, WorkerName);
            Console.WriteLine("У работяги новый bossOfGYM");
        }
    }

    public enum ReportMode
    {
        DayReport,
        BossReport,
        FinalReport
    }
}
