using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Worker
{
    public class ManageWorker// Слой логики
    {
        public void ChangeChief(Worker Person,Worker chief)
        {
            Person.Chief = chief;
        }

        public void GiveWorkers(Worker Chief, List<Worker> subWorkers)
        {
            Chief.SubWorkers = subWorkers;
        }
    }
}
