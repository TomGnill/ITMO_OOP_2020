using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportingSystem.Worker
{
    public class GiveWorkers : IManageWorkers// Слой логики~~~
    {
        public List<Worker> SubWorkers;
        public List<Worker> Workers;
        public Worker Chief;

        public GiveWorkers(List<Worker> subWorkers, List<Worker> workers, string ChiefName)
        {
            Workers = workers;
            SubWorkers = subWorkers;
            giveWorkers(ChiefName);
        }

        public void giveWorkers(string ChiefName)
        {

            foreach (var Boss in Workers)
            {
                if (Boss.Name == ChiefName)
                {
                    Chief = Boss;
                }
                
            }
            Action();
        }

        public Worker Action()
        {
            Chief.SubWorkers = SubWorkers; 
            foreach (var t in SubWorkers)
            {
                t.Chief = Chief;
            }

            return Chief;
        }
    }

    public class ChangeChief : IManageWorkers
    {
        public List<Worker> Workers;
        public Worker Person, Chief;

        public ChangeChief(List<Worker> workers, string ChiefName, string WorkerName)
        {
            Workers = workers;
            Action(ChiefName, WorkerName);
        }

        
        public void Action(string ChiefName,string WorkerName)
        {
            foreach (var boss in Workers.Where(boss => ChiefName == boss.Name))
            {
                Chief = boss;
                foreach (var worker in Workers.Where(worker => WorkerName == worker.Name))
                {
                    Person = worker;
                    Action();
                }
            }
        }

        public Worker Action()
        {
            Person.Chief = Chief;
            Chief.SubWorkers.Add(Person);
            return Chief;
        }
    }
}
