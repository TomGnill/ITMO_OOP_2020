using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Worker
{
    public class GiveWorkers : IManageWorkers// Слой логики
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
            for (int i = 0; i<SubWorkers.Count; i++)
            {
                SubWorkers[i].Chief = Chief;
            }

            return Chief;
        }
    }

    public class ChangeChief : IManageWorkers
    {
        public List<Worker> Workers;
        public Worker Workere, Chief;

        public ChangeChief(List<Worker> workers, string ChiefName, string WorkerName)
        {
            Workers = workers;
            Action(ChiefName, WorkerName);
        }

        
        public void Action(string ChiefName,string WorkerName)
        {
            foreach (var Boss in Workers)
            {
                if (ChiefName == Boss.Name)
                {
                    Chief = Boss;
                    foreach (var worker in Workers)
                    {
                        if (WorkerName == worker.Name)
                        {
                            Workere = worker;
                            Action();
                        }
                    }
                }
            }

        }

        public Worker Action()
        {
            Workere.Chief = Chief;
            Chief.SubWorkers.Add(Workere);
            return Chief;
        }
    }
}
