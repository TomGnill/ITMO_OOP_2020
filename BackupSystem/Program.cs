using System;
using System.Collections.Generic;

namespace BackupSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BackupSys newSys = new BackupSys();
            BackupFile.AbstractFile file1 = new BackupFile.AbstractFile("Test1", 100, DateTime.Now);
            BackupFile.AbstractFile file2 = new BackupFile.AbstractFile("Test2", 100, DateTime.Now);
            BackupFile.AbstractFile file3 = new BackupFile.AbstractFile("Test3", 100, DateTime.Now);
            newSys.AddFile(file1);
            newSys.AddFile(file2);
            newSys.AddFile(file3);
            List<BackupFile.AbstractFile> list = newSys.endEditing();
            newSys.SeparateBackup(list,Type.Full);
            newSys.DelFile(2);
            newSys.SeparateBackup(list,Type.Incremental);
            newSys.SeparateBackup(list, Type.Full);
            newSys.SeparateBackup(list, Type.Incremental);
            newSys.SeparateBackup(list, Type.Incremental);
            newSys.RemoveBySize(700);
            newSys.ShowPoints();
            newSys.ShowFilesInPoint(2);
        }
    }
}
