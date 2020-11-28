using System;
using System.Collections.Generic;
using System.Linq;


namespace BackupSystem
{
    public class RestoreSystem
    {
        public List<RestorePoint> Points = new List<RestorePoint>();

        public string AddPoint((long, List<FileRestoreCopyInfo>, Type) info)
        {
            RestorePoint newPoint = new RestorePoint(GetCorrectId(), DateTime.Now, info.Item1, info.Item2, info.Item3);
            Points.Add(newPoint);
            return "Точка восстановления создана";
        }

        public string AddPoint((long, Type, AbstractArchive) info)
        {
            RestorePoint newPoint = new RestorePoint(GetCorrectId(), DateTime.Now, info.Item1,info.Item3, info.Item2);
            Points.Add(newPoint);
            return "Точка восстановления создана";
        }

        public FileRestoreCopyInfo CreateRestore(AbstractFile filePath)
        {

            var fileRestoreCopyInfo = new FileRestoreCopyInfo(filePath.Filename, filePath.Filesize, DateTime.Now);
            //File.Copy(filePath, _pathWhereWeNeedToStoreOurBackup); <- Вот эту часть мы можем скипать
            return fileRestoreCopyInfo;
        }

        public List<RestorePoint> ShowRestorePoints()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Console.WriteLine($"ID: {Points[i].Id} , Размер бекапа: {Points[i].BackupSize}mb, дата создания {Points[i].CreationTime}, {Points[i].PointType}");
            }

            return Points;
        }

        public int GetCorrectId()
        {
            int id;
            if (Points.Count is 0)
            {
                id = 1;
            }
            else
            {
                id = Points[^1].Id + 1;
            }

            return id;
        }

        public List<FileRestoreCopyInfo> ShowRestoreFiles(int ID)
        {
            List<FileRestoreCopyInfo> restoreFiles = new List<FileRestoreCopyInfo>();
            foreach (var Rpoint in Points.Where(Rpoint => Rpoint.Id == ID))
            {
                if (Rpoint.BackupedFiles != null)
                {
                    foreach (FileRestoreCopyInfo info in Rpoint.BackupedFiles)
                    {
                        Console.WriteLine(
                            $"размер файла: {info.Size}mb, Имя файла :{info.FilePath}, дата создания {info.CreationTime}");
                        restoreFiles = Rpoint.BackupedFiles;
                    }
                }
                else
                {
                    Console.WriteLine("Эта точка не хранит отдельные данные о своих файлах");

                }
            }

            return restoreFiles;
        }
    }

    public interface ICleaningPoints
    {
        List<int> AnalyzePoints(List<RestorePoint> points); 

        public int Clean(List<RestorePoint> points);

        public void DeletePoints(int Count, List<RestorePoint> points);
    }
    

  public  class CleanByPoints : ICleaningPoints
  {
      public int MaxPoints;

      public CleanByPoints(int size)
      {
          MaxPoints = size;
      }

        public List<int> AnalyzePoints(List<RestorePoint> points)
        {
          List<int> WhatPointsToDelete = new List<int>();
          int amount = 1;
          for (int i = 1; i < points.Count; i++)
          {
              if (points[i].PointType == Type.Full)
              {
                  WhatPointsToDelete.Add(amount);
              }
              amount++;
          }

          WhatPointsToDelete.Add(amount);
          WhatPointsToDelete.Add(100);
          return WhatPointsToDelete;
        }
        public int Clean(List<RestorePoint> Points)
        {
            int pointsToDelete = Points.Count - MaxPoints;
            DeletePoints(pointsToDelete,Points);
            return pointsToDelete;
        }
        public void DeletePoints(int PointsToDelete, List<RestorePoint> points)
        {
            List<int> DelPoints = AnalyzePoints(points);
            for (int i = 0; i < DelPoints.Count; i++)
            {
                if ((DelPoints[i] <= PointsToDelete) && (DelPoints[i + 1] >= PointsToDelete))
                {
                    for (int j = 0; j < DelPoints[i]; j++)
                    {
                        points.RemoveAt(0);
                    }
                }
            }
        }
    }

  public class CleanBySize : ICleaningPoints
  {
      public long MaxSize;

      public CleanBySize(long Size)
      {
          MaxSize = Size;
      }
     public  int Clean(List<RestorePoint> Points)
     {
          long PointsSize = 0;
          int pointsToDelete = 0;
          for (int i = Points.Count - 1; i >= 0; i--)
          {
              PointsSize += Points[i].BackupSize;

              if (PointsSize > MaxSize)
              {
                  pointsToDelete++;
              }
          }

          DeletePoints(pointsToDelete,Points);
          return pointsToDelete;
      }
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


      public void DeletePoints(int PointsToDelete, List<RestorePoint> Points)
      {
          List<int> DelPoints = AnalyzePoints(Points);
          for (int i = 0; i < DelPoints.Count; i++)
          {
              if ((DelPoints[i] <= PointsToDelete) && ((DelPoints[i + 1] >= PointsToDelete) || (i == DelPoints.Count -1 )))
              {
                  for (int j = 0; j < DelPoints[i]; j++)
                  {
                      Points.RemoveAt(0);
                  }
              }
          }
      }

  }

  public class CleanByDate : ICleaningPoints
  {
      public DateTime MaxDate;

      public CleanByDate(DateTime date)
      {
          MaxDate = date;
      }
      
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

      public int Clean(List<RestorePoint> Points)
      {

          int pointsToDelete = 0;
          for (int i = 0; i < Points.Count; i++)
          {
              if (Points[i].CreationTime < MaxDate)
              {
                  pointsToDelete++;
              }
          }

          DeletePoints(pointsToDelete,Points);
          return pointsToDelete;
      }

        public void DeletePoints(int PointsToDelete, List<RestorePoint> Points)
        {
          List<int> DelPoints = AnalyzePoints(Points);
          for (int i = 0; i < DelPoints.Count; i++)
          {
              if ((DelPoints[i] <= PointsToDelete) && (DelPoints[i + 1] >= PointsToDelete))
              {
                  for (int j = 0; j < DelPoints[i]; j++)
                  {
                      Points.RemoveAt(0);
                  }
              }
          }
        }

    }

  public class HybridCleanOneTerm : ICleaningPoints
  {
  
      public DateTime MaxDate;
      public long MaxSize;
      public int MaxPoints;

        public HybridCleanOneTerm(int maxPoints,long size , DateTime date)
        { 
            MaxPoints = maxPoints;
          MaxSize = size;
          MaxDate = date;
        }
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

        public int Clean(List<RestorePoint> Points)
        {
            List<int> PointsToDelete = new List<int>();
            if (MaxPoints != 0)
            {
                int pointsToDelete = Points.Count - MaxPoints;
                PointsToDelete.Add(pointsToDelete);

            }

            if (MaxSize != 0)
            {
                long PointsSize = 0;
                int pointsToDelete = 0;
                for (int i = Points.Count - 1; i >= 0; i--)
                {
                    PointsSize += Points[i].BackupSize;

                    if (PointsSize > MaxSize)
                    {
                        pointsToDelete++;
                    }
                }

                PointsToDelete.Add(pointsToDelete);

            }

            if (MaxDate != DateTime.MinValue)
            {
                int pointsToDelete = 0;
                for (int i = 0; i < Points.Count; i++)
                {
                    if (Points[i].CreationTime < MaxDate)
                    {
                        pointsToDelete++;
                    }
                }

                PointsToDelete.Add(pointsToDelete);
            }
            DeletePoints(PointsToDelete.Max(),Points);
            return PointsToDelete.Max();
        }
        public void DeletePoints(int PointsToDelete, List<RestorePoint> Points)
        {
            List<int> DelPoints = AnalyzePoints(Points);
            for (int i = 0; i < DelPoints.Count; i++)
            {
                if ((DelPoints[i] <= PointsToDelete) && (DelPoints[i + 1] >= PointsToDelete))
                {
                    for (int j = 0; j < DelPoints[i]; j++)
                    {
                        Points.RemoveAt(0);
                    }
                }
            }
        }
  }

  public class HybridCleanAllTerm : ICleaningPoints
  {
      public DateTime MaxDate;
        public long MaxSize;
        public int MaxPoints;

        public HybridCleanAllTerm(int maxPoints, long size, DateTime date)
        {
            MaxPoints = maxPoints;
            MaxSize = size;
            MaxDate = date;
        }
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

        public int Clean(List<RestorePoint> Points)
        {
            List<int> PointsToDelete = new List<int>();
            if (MaxPoints != 0)
            {
                int pointsToDelete = Points.Count - MaxPoints;
                PointsToDelete.Add(pointsToDelete);

            }

            if (MaxSize != 0)
            {
                long PointsSize = 0;
                int pointsToDelete = 0;
                for (int i = Points.Count - 1; i >= 0; i--)
                {
                    PointsSize += Points[i].BackupSize;

                    if (PointsSize > MaxSize)
                    {
                        pointsToDelete++;
                    }
                }

                PointsToDelete.Add(pointsToDelete);

            }

            if (MaxDate != DateTime.MinValue)
            {
                int pointsToDelete = 0;
                for (int i = 0; i < Points.Count; i++)
                {
                    if (Points[i].CreationTime < MaxDate)
                    {
                        pointsToDelete++;
                    }
                }

                PointsToDelete.Add(pointsToDelete);
            }
            DeletePoints(PointsToDelete.Min(),Points);
            return PointsToDelete.Min();
        }
        public void DeletePoints(int PointsToDelete, List<RestorePoint> Points)
        {
            List<int> DelPoints = AnalyzePoints(Points);
            for (int i = 0; i < DelPoints.Count; i++)
            {
                if ((DelPoints[i] <= PointsToDelete) && (DelPoints[i + 1] >= PointsToDelete))
                {
                    for (int j = 0; j < DelPoints[i]; j++)
                    {
                        Points.RemoveAt(0);
                    }
                }
            }
        }

    }

    public class FileRestoreCopyInfo
    {
        public string FilePath;
        public long Size;
        public DateTime CreationTime;

        public FileRestoreCopyInfo(string filePath, long size, DateTime creationTime)
        {
            FilePath = filePath;
            Size = size;
            CreationTime = creationTime;
        }
    }

    public enum Type
    {
        Full,
        Incremental
    }

    public class RestorePoint
    {
        public int Id;
        public Type PointType;
        public DateTime CreationTime;
        public long BackupSize;
        public List<FileRestoreCopyInfo> BackupedFiles;
        public AbstractArchive Files;

        public RestorePoint(int id, DateTime time, long size, List<FileRestoreCopyInfo> files, Type type)
        {
            Id = id;
            BackupSize = size;
            BackupedFiles = new List<FileRestoreCopyInfo>(files);
            CreationTime = time;
            PointType = type;
        }

        public RestorePoint(int id, DateTime time, long size,AbstractArchive archive, Type type)
        {
            Id = id;
            BackupSize = size;
            CreationTime = time;
            Files = archive;
            PointType = type;
        }
    }

}