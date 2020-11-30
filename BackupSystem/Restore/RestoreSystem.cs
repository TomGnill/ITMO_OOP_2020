using System;
using System.Collections.Generic;
using System.Linq;
using BackupSystem.Restore;


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

    

}