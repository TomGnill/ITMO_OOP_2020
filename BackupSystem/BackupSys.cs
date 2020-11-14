using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace BackupSystem
{
    class BackupSys
    {
        public List<BackupFile.AbstractFile> BackupList;
        RestoreSysyem sysyem = new RestoreSysyem();

        public BackupSys()
        {
            BackupList = new List<BackupFile.AbstractFile>();
        }

        public int AddFile(BackupFile.AbstractFile file)
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
                Console.WriteLine($"{index} , {BackupList[index].Filename}");
            }
        }

        public List<BackupFile.AbstractFile> endEditing()
        {
            return BackupList;
        }

        BackupFile.FileRestoreCopyInfo CreateRestore(BackupFile.AbstractFile filePath)
        {

            var fileRestoreCopyInfo = new BackupFile.FileRestoreCopyInfo(filePath.Filename, filePath.Filesize, DateTime.Now);
            //File.Copy(filePath, _pathWhereWeNeedToStoreOurBackup); <- Вот эту часть мы можем скипать
            return fileRestoreCopyInfo;
        }

        public void SeparateBackup(List<BackupFile.AbstractFile> list) //Храним инфу про каждый файл который мы бекапим (раздельный бекап)
        {
            int count = sysyem.Points.Count;
            List<BackupFile.FileRestoreCopyInfo> files = new List<BackupFile.FileRestoreCopyInfo>(); //храним инфу про каждый файл
            BackupFile.FileRestoreCopyInfo newFile;
            long size=0;
            for (int index = 0; index < list.Count; index++)
            {
                size += list[index].Filesize;
                newFile =  CreateRestore(list[index]);
                files.Add(newFile);
            }

            if (sysyem.Points.Count is 0)
            {
                count = 0;
            }

            RestorePoint newPoint = new RestorePoint(count+1,DateTime.Now, size,files);
            sysyem.AddPoint(newPoint);
            Console.WriteLine($"Бекап прошёл успешно, файлы храняться в архиве");
        }

        public void GeneralBackup(List<BackupFile.AbstractFile> list) //бекапим все файлы и не храним инфу про каждый файл в бекапе(совместное хранение)
        {
            int count = sysyem.Points.Count;
            BackupFile.FileRestoreCopyInfo newFile;
            long size = 0;
            for (int index = 0; index < list.Count; index++)
            {
                size += list[index].Filesize;
                newFile = CreateRestore(list[index]);
            }

            if (sysyem.Points.Count is 0)
            {
                count = 0;
            }

            RestorePoint newPoint = new RestorePoint(count+1, DateTime.Now, size);
            sysyem.AddPoint(newPoint);
            Console.WriteLine($"Бекап прошёл успешно, файлы храняться в архиве");
        }

        public void ShowPoints()
        {
            sysyem.ShowRestorePoints();
        }

        public void ShowFilesInPoint(int ID)
        {
            sysyem.ShowRestoreFiles(ID);
        }
    }


    
}
