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

        public string AddPoint((long, Type) info)
        {
            RestorePoint newPoint = new RestorePoint(GetCorrectId(), DateTime.Now, info.Item1, info.Item2);
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
                Console.WriteLine(
                    $"ID: {Points[i].ID} , Размер бекапа: {Points[i].BackupSize}mb, дата создания {Points[i].CreationTime}, {Points[i].PointType}");
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
                id = Points[^1].ID + 1;
            }

            return id;
        }

        public List<FileRestoreCopyInfo> ShowRestoreFiles(int ID)
        {
            List<FileRestoreCopyInfo> restoreFiles = new List<FileRestoreCopyInfo>();
            foreach (var Rpoint in Points.Where(Rpoint => Rpoint.ID == ID))
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

    //Не придумал как алгоритмы отчистки выкинуть из основной системы( проблема в том, что алгоритму нужен список точек, и в принципе логично что работать с ним удобнее в самом классе, но если подскажешь как это пофиксить, буду благодарен)
      public class CleaningAlgoritms : RestoreSystem
      {
          public List<RestorePoint> Points;

          public CleaningAlgoritms(List<RestorePoint> Chain)
          {
              Points = Chain;
          }

            public string CleanByID(int maxPonts)
            {
                int pointsToDelete = Points.Count - maxPonts;
                DeletePoints(pointsToDelete);
                return "Отчистка завершена";
            }

            public string CleanBySize(long maxSize)
            {
                long PointsSize = 0;
                int pointsToDelete = 0;
                for (int i = Points.Count - 1; i >= 0; i--)
                {
                    PointsSize += Points[i].BackupSize;

                    if (PointsSize > maxSize)
                    {
                        pointsToDelete++;
                    }
                }

                DeletePoints(pointsToDelete);
                return "отчистка завершена";
            }

            public string CleanBeforeDate(DateTime maxDate)
            {

                int pointsToDelete = 0;
                for (int i = 0; i < Points.Count; i++)
                {
                    if (Points[i].CreationTime < maxDate)
                    {
                        pointsToDelete++;
                    }
                }

                DeletePoints(pointsToDelete);
                return "отчистка завершена";
            }

            public List<int> CleaningTerms(int maxPoints, long maxSize, DateTime maxDate)
            {
                List<int> PointsToDelete = new List<int>();
                if (maxPoints != 0)
                {
                    int pointsToDelete = Points.Count - maxPoints;
                    PointsToDelete.Add(pointsToDelete);

                }

                if (maxSize != 0)
                {
                    long PointsSize = 0;
                    int pointsToDelete = 0;
                    for (int i = Points.Count - 1; i >= 0; i--)
                    {
                        PointsSize += Points[i].BackupSize;

                        if (PointsSize > maxSize)
                        {
                            pointsToDelete++;
                        }
                    }

                    PointsToDelete.Add(pointsToDelete);

                }

                if (maxDate != DateTime.MinValue)
                {
                    int pointsToDelete = 0;
                    for (int i = 0; i < Points.Count; i++)
                    {
                        if (Points[i].CreationTime < maxDate)
                        {
                            pointsToDelete++;
                        }
                    }

                    PointsToDelete.Add(pointsToDelete);
                }

                return PointsToDelete;
            }

            public List<int> AnalyzePoints()
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

            public string HybridAll(List<int> PointsToDelete)
            {
                DeletePoints(PointsToDelete.Min());
                return "Отчистка завершена";
            }

            public string HybridOne(List<int> PointsToDelete)
            {
                DeletePoints(PointsToDelete.Max());
                return "Отчистка завершена";
            }

            public void DeletePoints(int PointsToDelete)
            {
                List<int> DelPoints = AnalyzePoints();
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
        public int ID;
        public Type PointType;
        public DateTime CreationTime;
        public long BackupSize;
        public List<FileRestoreCopyInfo> BackupedFiles;

        public RestorePoint(int id, DateTime time, long size, List<FileRestoreCopyInfo> files, Type type)
        {
            ID = id;
            BackupSize = size;
            BackupedFiles = new List<FileRestoreCopyInfo>(files);
            CreationTime = time;
            PointType = type;
        }

        public RestorePoint(int id, DateTime time, long size, Type type)
        {
            ID = id;
            BackupSize = size;
            CreationTime = time;
            PointType = type;
        }
    }

}