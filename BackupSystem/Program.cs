using System;
using System.Collections.Generic;

namespace BackupSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BackupSys newSys = new BackupSys();
            string test1 = "Test1.txt";
            string test2 = "Test2.txt";
            string test3 = "Test3.txt";
            newSys.AddFile(test1);
            newSys.AddFile(test2);
            newSys.AddFile(test3);
            newSys.DelFile(1);
            newSys.ReadList();
            List<string> list = newSys.endEditing();
            newSys.GeneralBackup(list);
        }
    }
}
