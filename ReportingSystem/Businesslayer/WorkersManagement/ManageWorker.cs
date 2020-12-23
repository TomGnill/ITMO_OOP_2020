using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Businesslayer.WorkersManagement
{
    public class GiveWorkers : IManageWorkers// Слой логики~~~
    {
        public List<Worker> SubWorkers;
        public Worker Chief;

        public GiveWorkers(List<Worker> subWorkers,  string ChiefName)
        {
            SubWorkers = subWorkers;
            giveWorkers(ChiefName);
        }

        public void giveWorkers(string ChiefName)
        {

            using (WorkerContext db = new WorkerContext())
            {
                Chief = db.Workers.Find(ChiefName);
            }

            Action();
        }

        public Worker Action()
        {
            using (WorkerContext db = new WorkerContext())
            {
                Chief.SubWorkers = SubWorkers;
                db.Entry(Chief).State = EntityState.Modified;
                foreach (var t in db.Workers)
                {
                    t.Chief = Chief;
                    db.Entry(t).State = EntityState.Modified;
                }
            }

            return Chief;
        }
    }

    public class ChangeChief : IManageWorkers
    {
    
        public Worker Person, Chief;

        public ChangeChief( string ChiefName, string WorkerName)
        {
            Action(ChiefName, WorkerName);
        }

        
        public void Action(string ChiefName,string WorkerName)
        {
            using (WorkerContext db = new WorkerContext())
            {
                Chief = db.Workers.Find(ChiefName);
                Person = db.Workers.Find(WorkerName);
            }

            Action();
        }

        public Worker Action()
        {
            using WorkerContext db = new WorkerContext();
            Chief.SubWorkers.Add(Person);
            db.Entry(Chief).State = EntityState.Modified;
            Person.Chief = Chief;
            db.Entry(Person).State = EntityState.Modified;
            db.SaveChanges();
            return Chief;
        }
    }
}
