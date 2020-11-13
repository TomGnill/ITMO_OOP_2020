using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace BackupSystem
{
    class BackupSys
    {
        public List<string> BackupList;

        public BackupSys()
        {
            BackupList = new List<string>();
        }

        public int AddFile(string file)
        {

            BackupList.Add(file);
            int Count = BackupList.Count;
            return Count;

        }

        public int DelFile(int fileNum)
        {
            BackupList.RemoveAt(fileNum);
            int Count = BackupList.Count;
            return Count;
        }

        public void ReadList()
        {
            for (int index = 0; index < BackupList.Count; index++)
            {
                Console.WriteLine($"{index} , {BackupList[index]}");
            }
        }

        public List<string> endEditing()
        {
            return BackupList;
        }

        public void BackupFile(string filePath, string path)
        {
           File.Copy(filePath, path, true);
        }

        public void FullBackup(List<string> list)
        {
            string sourcePath = @"C:\sourceBS";
            string targetPath = @$"C:\backup\Новый бекап({DateTime.Now.Hour})";
            string toZip = @$"C:\backup\NewBackup({DateTime.Now.Hour}).zip";
            DirectoryInfo dirInfo = new DirectoryInfo(targetPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            for (int index = 0; index < list.Count; index++)
            {
                string sourceFile = Path.Combine(sourcePath, list[index]);
                string backapedFile = Path.Combine(targetPath, list[index]);

                BackupFile(sourceFile, backapedFile);
            }

            ZipFile.CreateFromDirectory(targetPath, toZip);
        }


    }


    class RestoreSysyem 
    {
        
    }
}
