using System.Collections.Generic;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.SearchTask
{
    public interface IExecuteSearch
    { 
        public List<DataAccesslayer.Task.Task> ReturnTasks();
    }
}
