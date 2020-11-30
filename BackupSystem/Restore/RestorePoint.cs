using System;
using System.Collections.Generic;
using System.Text;

namespace BackupSystem.Restore
{
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

        public RestorePoint(int id, DateTime time, long size, AbstractArchive archive, Type type)
        {
            Id = id;
            BackupSize = size;
            CreationTime = time;
            Files = archive;
            PointType = type;
        }
    }
}
