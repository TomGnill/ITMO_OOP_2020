using System;
using System.Collections.Generic;

namespace BackupSystem
{

    public class AbstractFile 
    {
        public string Filename;
        public long Filesize;
        public DateTime FileRedTime;

        public AbstractFile(string name, long size, DateTime time)
        {
            Filename = name;
            Filesize = size;
            FileRedTime = time;
        }
    }
    public class AbstractArchive
    {
       
        public long Size;
        public DateTime FileRedTime;
        private List<AbstractFile> files;

        public AbstractArchive( long size, DateTime time, List<AbstractFile> list)
        {
            Size = size;
            FileRedTime = time;
            files = list;
        }
    }

    public class BackupInfo
    {
        public long Size;
        public List<FileRestoreCopyInfo> fileList;
        public Type poinType;
        public AbstractArchive archive;

        public BackupInfo(long size, Type type, List<FileRestoreCopyInfo> list)
        {
            Size = size;
            poinType = type;
            fileList = list;
        }

        public BackupInfo(long size, Type type, AbstractArchive list)
        {
            Size = size;
            poinType = type;
            archive = list;
        }

    }

}
