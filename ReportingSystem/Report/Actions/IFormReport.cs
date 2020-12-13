using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Report.Actions
{
   public interface IFormReport
   {
       public List<TaskInfo> ReturnReport();
   }
}
