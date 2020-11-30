using System;
using System.Collections.Generic;
using System.Text;
using BackupSystem.Restore;

namespace BackupSystem.Cleaning
{
    public class CleanBySize : ICleaningPoints
    {
        public long MaxSize;
        public CleanBySize(long Size)
        {
            MaxSize = Size;
        }
        public int Clean(List<RestorePoint> points)
        {
            long pointsSize = 0;
            int pointsToDelete = 0;
            for (int i = points.Count - 1; i >= 0; i--)
            {
                pointsSize += points[i].BackupSize;

                if (pointsSize > MaxSize)
                {
                    pointsToDelete++;
                }
            }
            return pointsToDelete;
        }
    }
}
