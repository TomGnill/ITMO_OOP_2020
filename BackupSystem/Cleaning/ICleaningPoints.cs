using System;
using System.Collections.Generic;
using System.Text;
using BackupSystem.Restore;

namespace BackupSystem.Cleaning
{
    public interface ICleaningPoints
    { 
        public List<int> AnalyzePoints(List<RestorePoint> Points)
        {
            List<int> WhatPointsToDelete = new List<int>();
            int amount = 1;
            for (int i = 1; i < Points.Count; i++)
            {
                if (Points[i].PointType == Type.Full)
                {
                    WhatPointsToDelete.Add(amount);
                }

                amount++;
            }

            WhatPointsToDelete.Add(amount);
            return WhatPointsToDelete;
        }

        public int Clean(List<RestorePoint> points);

        public void DeletePoints(int PointsToDelete, List<RestorePoint> Points)
        {
            List<int> DelPoints = AnalyzePoints(Points);
            for (int i = 0; i < DelPoints.Count; i++)
            {
                if ((DelPoints[i] <= PointsToDelete) && ((DelPoints[i + 1] >= PointsToDelete) || (i == DelPoints.Count - 1)))
                {
                    for (int j = 0; j < DelPoints[i]; j++)
                    {
                        Points.RemoveAt(0);
                    }
                }
            }
        }

        public void StartClean(List<RestorePoint> points)
        {
            int del = Clean(points);
            DeletePoints(del, points);
        }
    }
}
