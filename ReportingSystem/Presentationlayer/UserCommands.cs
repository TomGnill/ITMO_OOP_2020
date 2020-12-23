using System;
using System.Collections.Generic;
using System.Linq;
using ReportingSystem.Businesslayer.FormReport;
using ReportingSystem.Businesslayer.TaskManagementSystem;
using ReportingSystem.DataAccesslayer.Report;
using ReportingSystem.DataAccesslayer.Task;
using ReportingSystem.DataAccesslayer.Worker;
using ReportingSystem.Task;

namespace ReportingSystem.Presentationlayer
{
    public class UserCommands // пользовательский слой
    {
        public ITaskSystem System;
        public TaskSystem NewSystem;

        public UserCommands()
        {
            NewSystem = new TaskSystem();
            System = NewSystem;
        }
        public IFormReport GenerateNewReport(ReportMode mode, Worker worker, DateTime date)//подумац
        {
            switch (mode)
            {
                case ReportMode.DayReport:
                    return System.FormDayReport(worker, date);
                    
                case ReportMode.BossReport:
                   return System.FormBossReport(worker, date);
                   
                case ReportMode.FinalReport:
                  return System.FormFinalReport(worker, date);
            }
            return null;
        } 
        public void PrintNewReport(IFormReport report)
        {
            var newReport = report.GenerateReport();
            if (newReport.report.Count == 0) return;
            newReport.GenerateInString();
        }

        public Report GetReport(IFormReport report)
        {
            var newReport = report.GenerateReport();
            return newReport;
        }

        public void PrintLog()
        {
            foreach (var taskInfo in NewSystem.Log)
            {
                if (taskInfo.Something == TaskInfo.Option.WorkerDoSomething)
                {
                    Console.WriteLine($"Работяга {taskInfo.ResolvedTask.Responsible.Name} внёс изменения в задачу {taskInfo.ResolvedTask.TaskName} ({taskInfo.LastEditTime})");
                }
                if (taskInfo.Something == TaskInfo.Option.ChangeWorker)
                {
                    Console.WriteLine($"У задачи {taskInfo.ResolvedTask.TaskName}, сменился исполнитель на {taskInfo.ResolvedTask.Responsible.Name}");
                }
                if (taskInfo.Something == TaskInfo.Option.Creating)
                {
                    Console.WriteLine($"Создана задача {taskInfo.ResolvedTask.TaskName} {taskInfo.CreateTime.Date}");
                }

                if (taskInfo.Something == TaskInfo.Option.AddComment)
                {
                    Console.WriteLine($"пользователь {taskInfo.ResolvedTask.Responsible.Name} прокомментировал задачу {taskInfo.ResolvedTask.TaskName}: {taskInfo.Comment}");
                }
                if (taskInfo.Something == TaskInfo.Option.ChangeStatus)
                {
                    Console.WriteLine($"пользоватенль {taskInfo.ResolvedTask.Responsible.Chief.Name} пометил задачу {taskInfo.ResolvedTask.TaskName} как {taskInfo.ResolvedTask.Status}");
                }
                
            }
        }

        public void SearchById(uint id)
        {
           var newList = System.SearchById(id).ReturnTasks();
           foreach (var task in newList)
           {
               Console.WriteLine($"Имя задачи {task.TaskName}");
           }
        }

        public void SearchByEdit(string name)
        {
            using (WorkerContext db = new WorkerContext())
            {
                foreach (var task in db.Workers.Where(worker => worker.Name == name)
                    .Select(worker => System.SearchByEdit(worker).ReturnTasks()).SelectMany(newList => newList))
                {
                    Console.WriteLine($"Имя задачи {task.TaskName}");
                }
            }
        }

       public void SearchByWorker(string Name)
       {
           using (WorkerContext db = new WorkerContext())
           {
               foreach (var task in db.Workers.Where(worker => worker.Name == Name)
                   .Select(worker => System.SearchByWorker(worker).ReturnTasks()).SelectMany(newList => newList))
               {
                   Console.WriteLine($"Имя задачи {task.TaskName}");
               }
           }
       }

        public void SearchByCreatingDate(DateTime date)
        {
            var newList = System.SearchByCreateDate(date).ReturnTasks();
            foreach (var task in newList)
            {
                Console.WriteLine($"Имя задачи {task.TaskName}");
            }
        }

        public void SearchLastEditingDate(DateTime date)
        {
            var newList = System.SearchByLastEditDate(date).ReturnTasks();
            foreach (var task in newList)
            {
                Console.WriteLine($"Имя задачи {task.TaskName}");
            }
        }

        public void ChangesInCode(uint id)
        {
            foreach (var task in NewSystem.TaskList.Where(task => task.Value == id))
            {
                System.WorkerDoSomething(task.Key);
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
            foreach (var task in NewSystem.TaskList.Where(task => task.Value == id))
            {
                System.AddComment(task.Key, comment);
            }

            Console.WriteLine("Задача прокомментирована");
        }

        public void ChangeWorker(uint id, string WorkerName)
        {
            
            Worker Worker = null;
            using (WorkerContext db = new WorkerContext())
            {
                Worker = db.Workers.Find(WorkerName);
            }
            
            foreach (var task in NewSystem.TaskList.Where(task => task.Value == id))
            {
                System.ChangeWorker(task.Key, Worker);
            }
        }

        public void ChangeTaskStatus(uint id, TaskStatus status)
        {
            foreach (var task in NewSystem.TaskList.Where(task => task.Value == id))
            {
                System.ChangeStatus(task.Key, status);
            }
        }

        public Worker CreateWorker(string Name)
        {
            var worker = NewSystem.CreateWorker(Name);
            Console.WriteLine("Работяга создан");
            return worker;
        }

        public void GiveWorkeres(string chiefName, List<Worker> workers)
        {
            System.GiveWorkers(chiefName, workers);
            Console.WriteLine("Работяги переданы");
        }

        public void ChangeChief(string chiefName, string workerName)
        {
            System.GiveChief(chiefName, workerName);
            Console.WriteLine("У работяги новый bossOfGYM");
        }

        public void PrintWorkersHierarchy()//tree
        {
            using (WorkerContext db = new WorkerContext())
            {
                foreach (var person in db.Workers)
                {
                    Console.Write($" {person.Name} ");
                }

                Console.WriteLine("\n");
                foreach (var Person in db.Workers)
                {

                    foreach (var subWorkers in db.Workers)
                    {
                        Console.Write(Person.SubWorkers.Contains(subWorkers) ? "    1    " : "    0    ");
                    }

                    Console.WriteLine($" {Person.Name} ");
                }
            }

        }
    }

    public enum ReportMode
    {
        DayReport,
        BossReport,
        FinalReport
    }
}
