using System;
using System.Collections.Generic;
using System.Text;
using BackupSystem.Restore;

namespace BackupSystem.Cleaning
{
    public class CleanByPoints : ICleaningPoints
    {
        public int MaxPoints;

        public CleanByPoints(int size)
        {
            MaxPoints = size;
        }

        public int Clean(List<RestorePoint> points)
        {
            int pointsToDelete = points.Count - MaxPoints;
            return pointsToDelete;
        }
        
    }
}
