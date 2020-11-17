using System;

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
   
}
