﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using BackupSystem;
using Type = System.Type;

namespace BackupSystem
{
    public class FileManager
    {
        public List<AbstractFile> ListToBackup;
        public FileManager()
        {
            ListToBackup = new List<AbstractFile>();
        }

        public int AddFile(AbstractFile file)
        {
            ListToBackup.Add(file);
            int Count = ListToBackup.Count;
            return Count;
        }

        public int DelFile(int fileNum)
        {
            ListToBackup.RemoveAt(fileNum);
            int Count = ListToBackup.Count;
            return Count;
        }

        public void WriteList()
        {
            for (int index = 0; index < ListToBackup.Count; index++)
            {
                Console.WriteLine($"{index} , {ListToBackup[index].Filename}");
            }
        }
        public List<AbstractFile> EndEditing()
        {
            return ListToBackup;
        }
    }
    public class StorageAlgorithms : RestoreSystem
    {
       
        public (long,List<FileRestoreCopyInfo>, Type)  SeparateBackup(List<AbstractFile> list, Type type) //Храним инфу про каждый файл который мы бекапим (раздельный бекап)
        {
          
            List<FileRestoreCopyInfo> files = new List<FileRestoreCopyInfo>(); //храним инфу про каждый файл
            long size = 0;
            foreach (var t in list)
            {
                size += t.Filesize;
                var newFile = CreateRestore(t);
                files.Add(newFile);
            }


            (long, List<FileRestoreCopyInfo>, Type) restoreInfo = (size, files, type);
            return restoreInfo;
        }

        public (long, Type) GeneralBackup(List<AbstractFile> list, Type type) //бекапим все файлы и не храним инфу про каждый файл в бекапе(совместное хранение)
        {
            FileRestoreCopyInfo newFile;
            long size = 0;
            foreach (var t in list)
            {
                size += t.Filesize;
                newFile = CreateRestore(t);
            }

            int id = GetCorrectId();
            (long, Type) restoreInfo = (size, type);
            return restoreInfo;
        }

    }
}

  



