using System;
using System.Collections.Generic;
using System.Threading;
using BackupSystem;
using NUnit.Framework;
using Type = BackupSystem.Type;

namespace Lab4_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
//Кейс #1
        [Test()]
        public void Test1()
        {
            BackupSys newSys = new BackupSys();
            BackupFile.AbstractFile file1 = new BackupFile.AbstractFile("Test1", 100, DateTime.Now);
            BackupFile.AbstractFile file2 = new BackupFile.AbstractFile("Test2", 100, DateTime.Now);
            newSys.AddFile(file1);
            newSys.AddFile(file2);
            List<BackupFile.AbstractFile> list = newSys.endEditing();
            newSys.SeparateBackup(list, Type.Full);
            newSys.SeparateBackup(list, Type.Full);
            newSys.RemoveByID(1);
            Assert.AreEqual(1, newSys.ShowPoints().Count);
        }

//Кейс #2
        [Test()]
        public void Test2()
        {
            BackupSys newSys = new BackupSys();
            BackupFile.AbstractFile file1 = new BackupFile.AbstractFile("Test1", 100, DateTime.Now);
            BackupFile.AbstractFile file2 = new BackupFile.AbstractFile("Test2", 100, DateTime.Now);
            newSys.AddFile(file1);
            newSys.AddFile(file2);
            List<BackupFile.AbstractFile> list = newSys.endEditing();
            newSys.SeparateBackup(list, Type.Full);
            newSys.SeparateBackup(list, Type.Full);
            newSys.RemoveBySize(250); //Не совсем понимаю почему должен остаться один бекап если мы закидываем два FULL Backup(каждый по 200мб), то теоретически мы их можем удалять без последствий и удалим каждый с весом >150
            Assert.AreEqual(1, newSys.ShowPoints().Count);
        }

 //Тесты на хранение       
        [Test()]
        public void Test3() //Тест на раздельное хранение. Узнаём что все файлы дошли до конца.
        {
            BackupSys newSys = new BackupSys();
            BackupFile.AbstractFile file1 = new BackupFile.AbstractFile("Test1", 100, DateTime.Now);
            BackupFile.AbstractFile file2 = new BackupFile.AbstractFile("Test2", 100, DateTime.Now);
            newSys.AddFile(file1);
            newSys.AddFile(file2);
            List<BackupFile.AbstractFile> list = newSys.endEditing();
            newSys.SeparateBackup(list, Type.Full);
            List<BackupFile.FileRestoreCopyInfo> RestoredFiles = newSys.ShowFilesInPoint(1);
            Assert.AreEqual(2, RestoredFiles.Count);
        }

        [Test()]
        public void Test4()
        {
            BackupSys newSys = new BackupSys();
            BackupFile.AbstractFile file1 = new BackupFile.AbstractFile("Test1", 100, DateTime.Now);
            BackupFile.AbstractFile file2 = new BackupFile.AbstractFile("Test2", 100, DateTime.Now);
            newSys.AddFile(file1);
            newSys.AddFile(file2);

            List<BackupFile.AbstractFile> list = newSys.endEditing();
            newSys.GeneralBackup(list, Type.Full);
            List<RestorePoint> RestoredFiles = newSys.ShowPoints();

            Assert.AreEqual(200, RestoredFiles[0].BackupSize);
        }

//тесты на гибридные режимы отчистки
        [Test()]
        public void Test5()//комбинируем дату и количество (должны выполняться все условия)
        {
            BackupSys newSys = new BackupSys();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);
            DateTime date2 = new DateTime(2020, 10, 20, 12, 30, 30);

            BackupFile.AbstractFile file1 = new BackupFile.AbstractFile("Test1", 100, date1);
            BackupFile.AbstractFile file2 = new BackupFile.AbstractFile("Test2", 100, date1);
            BackupFile.AbstractFile file3 = new BackupFile.AbstractFile("Test3", 100, date1);

            BackupFile.AbstractFile file4 = new BackupFile.AbstractFile("Test1_a", 100, date2);
            BackupFile.AbstractFile file5 = new BackupFile.AbstractFile("Test2_a", 100, date2);
            BackupFile.AbstractFile file6 = new BackupFile.AbstractFile("Test3_a", 100, date2);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            newSys.AddFile(file3);

            List<BackupFile.AbstractFile> list = newSys.endEditing();

            newSys.SeparateBackup(list, Type.Full);

            newSys.DelFile(2);

            newSys.SeparateBackup(list, Type.Incremental);

            newSys.DelFile(0);
            newSys.DelFile(0);

            newSys.AddFile(file4);
            newSys.AddFile(file5);
            newSys.AddFile(file6);

            List<BackupFile.AbstractFile> list1 = newSys.endEditing();

            Thread.Sleep(5000);

            DateTime maxDate = DateTime.Now;

            Thread.Sleep(10000);

            newSys.SeparateBackup(list1, Type.Full);
            newSys.SeparateBackup(list1, Type.Incremental);
            newSys.SeparateBackup(list1, Type.Incremental);


            newSys.SeparateBackup(list1, Type.Full);
            newSys.SeparateBackup(list1, Type.Incremental);
            newSys.SeparateBackup(list1, Type.Incremental);

            newSys.RemoveIfAllTermTrue(3,0,maxDate);

            Assert.AreEqual(6, newSys.ShowPoints().Count);
        }
        [Test()]
        public void Test6()//комбинируем количество и размер (должно выполняться одно из условий )
        {
            BackupSys newSys = new BackupSys();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);
            DateTime date2 = new DateTime(2020, 10, 20, 12, 30, 30);

            BackupFile.AbstractFile file1 = new BackupFile.AbstractFile("Test1", 100, date1);
            BackupFile.AbstractFile file2 = new BackupFile.AbstractFile("Test2", 100, date1);
            BackupFile.AbstractFile file3 = new BackupFile.AbstractFile("Test3", 100, date1);

            BackupFile.AbstractFile file4 = new BackupFile.AbstractFile("Test1_a", 100, date2);
            BackupFile.AbstractFile file5 = new BackupFile.AbstractFile("Test2_a", 100, date2);
            BackupFile.AbstractFile file6 = new BackupFile.AbstractFile("Test3_a", 100, date2);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            newSys.AddFile(file3);

            List<BackupFile.AbstractFile> list = newSys.endEditing();

            newSys.SeparateBackup(list, Type.Full);

            newSys.DelFile(2);

            newSys.SeparateBackup(list, Type.Incremental);

            newSys.DelFile(0);
            newSys.DelFile(0);

            newSys.AddFile(file4);
            newSys.AddFile(file5);
            newSys.AddFile(file6);

            List<BackupFile.AbstractFile> list1 = newSys.endEditing();

            newSys.SeparateBackup(list1, Type.Full);
            newSys.SeparateBackup(list1, Type.Incremental);
            newSys.SeparateBackup(list1, Type.Incremental);


            newSys.SeparateBackup(list1, Type.Full);
            newSys.SeparateBackup(list1, Type.Incremental);
            newSys.SeparateBackup(list1, Type.Incremental);

            newSys.RemoveIfOneTermTrue(4, 1000, DateTime.MinValue);

            Assert.AreEqual(3, newSys.ShowPoints().Count);
        }
    }
}