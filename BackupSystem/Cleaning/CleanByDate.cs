using System;
using System.Collections.Generic;
using System.Text;
using BackupSystem.Restore;

namespace BackupSystem.Cleaning
{
    public class CleanByDate : ICleaningPoints
    {
        public DateTime MaxDate;

        public CleanByDate(DateTime date)
        {
            MaxDate = date;
        }
        public int Clean(List<RestorePoint> points)
        {
            int pointsToDelete = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].CreationTime < MaxDate)
                {
                    pointsToDelete++;
                }
            }
            return pointsToDelete;
        }
    }
}
