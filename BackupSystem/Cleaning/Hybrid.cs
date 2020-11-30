using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackupSystem.Restore;

namespace BackupSystem.Cleaning
{
    public class HybridCleanAllTerm : ICleaningPoints
    {
        List<ICleaningPoints> MaxDel;

        public HybridCleanAllTerm(List<ICleaningPoints> maxDel)
        {
            MaxDel = maxDel;
        }
        
        public int Clean(List<RestorePoint> points)
        {
            List<int> pointsToDelete = new List<int>();
            int pointsToDel;

            for (int i = 0; i < MaxDel.Count; i++)
            {
                pointsToDel = MaxDel[i].Clean(points);
                pointsToDelete.Add(pointsToDel);
            }
            return pointsToDelete.Min();
        }
       
    }
    public class HybridCleanOneTerm : ICleaningPoints
    {

        List<ICleaningPoints> MaxDel;

        public HybridCleanOneTerm(List<ICleaningPoints> maxDel)
        {
            MaxDel = maxDel;
        }
        public int Clean(List<RestorePoint> points)
        {
            List<int> pointsToDelete = new List<int>();
            int pointsToDel;
            for (int i = 0; i < MaxDel.Count; i++)
            {
                pointsToDel = MaxDel[i].Clean(points);
                pointsToDelete.Add(pointsToDel);
            }
            return pointsToDelete.Max();
        }
    }
}
