using System;
using System.Collections.Generic;
using System.Threading;
using BackupSystem;
using BackupSystem.Cleaning;
using BackupSystem.Restore;
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
            FileManager newSys = new FileManager();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);

            AbstractFile file1 = new AbstractFile("Test1", 100, date1);
            AbstractFile file2 = new AbstractFile("Test2", 100, date1);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            List<AbstractFile> list = newSys.EndEditing();
            StorageAlgorithms algorithm = new StorageAlgorithms();

            var pointInfo1 = algorithm.SeparateBackup(list, Type.Full);
            var pointInfo2 = algorithm.SeparateBackup(list, Type.Full);

            RestoreSystem newChain = new RestoreSystem();
            newChain.AddPoint(pointInfo1);
            newChain.AddPoint(pointInfo2);
            ICleaningPoints Clean = new CleanByPoints(1);

            Clean.Clean(newChain.Points);
            Assert.AreEqual(1, newChain.ShowRestorePoints().Count);
            Assert.AreEqual(1, newChain.ShowRestorePoints().Count);
        }

//Кейс #2
        [Test()]
        public void Test2()
        {
            FileManager newSys = new FileManager();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);

            AbstractFile file1 = new AbstractFile("Test1", 100, date1);
            AbstractFile file2 = new AbstractFile("Test2", 100, date1);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            List<AbstractFile> list = newSys.EndEditing();
            StorageAlgorithms algorithm = new StorageAlgorithms();

            var pointInfo1 = algorithm.SeparateBackup(list, Type.Full);
            var pointInfo2 = algorithm.SeparateBackup(list, Type.Full);

            RestoreSystem newChain = new RestoreSystem();
            newChain.AddPoint(pointInfo1);
            newChain.AddPoint(pointInfo2);
            ICleaningPoints Clean = new CleanBySize(250);
         
            Clean.StartClean(newChain.Points); //Не совсем понимаю почему должен остаться один бекап если мы закидываем два FULL Backup(каждый по 200мб), то теоретически мы их можем удалять без последствий и удалим каждый с весом >150
            Assert.AreEqual(1, newChain.ShowRestorePoints().Count);
        }

 //Тесты на хранение       
      [Test()]
        public void Test3() //Тест на раздельное хранение. Узнаём что все файлы дошли до конца.
        {
            FileManager newSys = new FileManager();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);

            AbstractFile file1 = new AbstractFile("Test1", 100, date1);
            AbstractFile file2 = new AbstractFile("Test2", 100, date1);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            List<AbstractFile> list = newSys.EndEditing();
            StorageAlgorithms algorithm = new StorageAlgorithms();

            var pointInfo1 = algorithm.SeparateBackup(list, Type.Full);

            RestoreSystem newChain = new RestoreSystem();
            newChain.AddPoint(pointInfo1);
            List<FileRestoreCopyInfo> restoredFiles = newChain.ShowRestoreFiles(1);
            Assert.AreEqual(2, restoredFiles.Count);
        }
  
        [Test()]
        public void Test4()
        {
            FileManager newSys = new FileManager();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);

            AbstractFile file1 = new AbstractFile("Test1", 100, date1);
            AbstractFile file2 = new AbstractFile("Test2", 100, date1);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            List<AbstractFile> list = newSys.EndEditing();
            StorageAlgorithms algorithm = new StorageAlgorithms();

            var pointInfo1 = algorithm.SeparateBackup(list, Type.Full);

            RestoreSystem newChain = new RestoreSystem();
            newChain.AddPoint(pointInfo1);
            List<RestorePoint> restoredFiles = newChain.ShowRestorePoints();

            Assert.AreEqual(200, restoredFiles[0].BackupSize);
        }

//тесты на гибридные режимы отчистки
        [Test()]
        public void Test5()//комбинируем дату и количество (должны выполняться все условия)
        {
            FileManager newSys = new FileManager();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);
            DateTime date2 = new DateTime(2020, 10, 20, 12, 30, 30);

            AbstractFile file1 = new AbstractFile("Test1", 100, date1);
            AbstractFile file2 = new AbstractFile("Test2", 100, date1);
            AbstractFile file3 = new AbstractFile("Test3", 100, date1);

            AbstractFile file4 = new AbstractFile("Test1_a", 100, date2);
            AbstractFile file5 = new AbstractFile("Test2_a", 100, date2);
            AbstractFile file6 = new AbstractFile("Test3_a", 100, date2);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            newSys.AddFile(file3);

            List<AbstractFile> list = newSys.EndEditing();


            StorageAlgorithms algorithm = new StorageAlgorithms();

            RestoreSystem newChain = new RestoreSystem();

            var pointInfo1 = algorithm.SeparateBackup(list, Type.Full);

            newChain.AddPoint(pointInfo1);

            newSys.DelFile(2);

            var pointInfo2 = algorithm.SeparateBackup(list, Type.Incremental);

            newChain.AddPoint(pointInfo2);

            newSys.DelFile(0);
            newSys.DelFile(0);

            newSys.AddFile(file4);
            newSys.AddFile(file5);
            newSys.AddFile(file6);

            List<AbstractFile> list1 = newSys.EndEditing();

            Thread.Sleep(5000);

            DateTime maxDate = DateTime.Now;

            Thread.Sleep(10000);

            var pointInfo3 = algorithm.SeparateBackup(list1, Type.Full);
            newChain.AddPoint(pointInfo3);

            var pointInfo4 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo4);

            var pointInfo5 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo5);


            var pointInfo6 = algorithm.SeparateBackup(list1, Type.Full);
            newChain.AddPoint(pointInfo6);

            var pointInfo7 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo7);

            var pointInfo8 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo8);

            List<ICleaningPoints> Hybrid1 = new List<ICleaningPoints>();
            ICleaningPoints cleanByPoints = new CleanByPoints(3);
            ICleaningPoints CleanByDate = new CleanByDate(maxDate);

            Hybrid1.Add(cleanByPoints);
            Hybrid1.Add(CleanByDate);


            ICleaningPoints HybrydAllTerm = new HybridCleanAllTerm(Hybrid1);


            HybrydAllTerm.StartClean(newChain.Points);
            Assert.AreEqual(6, newChain.ShowRestorePoints().Count);
        }
        [Test()]
        public void Test6()//комбинируем количество и размер (должно выполняться одно из условий )
        {
            FileManager newSys = new FileManager();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);
            DateTime date2 = new DateTime(2020, 10, 20, 12, 30, 30);

            AbstractFile file1 = new AbstractFile("Test1", 100, date1);
            AbstractFile file2 = new AbstractFile("Test2", 100, date1);
            AbstractFile file3 = new AbstractFile("Test3", 100, date1);

            AbstractFile file4 = new AbstractFile("Test1_a", 100, date2);
            AbstractFile file5 = new AbstractFile("Test2_a", 100, date2);
            AbstractFile file6 = new AbstractFile("Test3_a", 100, date2);

            newSys.AddFile(file1);
            newSys.AddFile(file2);
            newSys.AddFile(file3);

            List<AbstractFile> list = newSys.EndEditing();


            StorageAlgorithms algorithm = new StorageAlgorithms();

            RestoreSystem newChain = new RestoreSystem();

            var pointInfo1 = algorithm.SeparateBackup(list, Type.Full);

            newChain.AddPoint(pointInfo1);

            newSys.DelFile(2);

            var pointInfo2 = algorithm.SeparateBackup(list, Type.Incremental);

            newChain.AddPoint(pointInfo2);

            newSys.DelFile(0);
            newSys.DelFile(0);

            newSys.AddFile(file4);
            newSys.AddFile(file5);
            newSys.AddFile(file6);

            List<AbstractFile> list1 = newSys.EndEditing();

            var pointInfo3 = algorithm.SeparateBackup(list1, Type.Full);
            newChain.AddPoint(pointInfo3);

            var pointInfo4 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo4);

            var pointInfo5 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo5);


            var pointInfo6 = algorithm.SeparateBackup(list1, Type.Full);
            newChain.AddPoint(pointInfo6);

            var pointInfo7 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo7);

            var pointInfo8 = algorithm.SeparateBackup(list1, Type.Incremental);
            newChain.AddPoint(pointInfo8);


            List<ICleaningPoints> Hybrid2 = new List<ICleaningPoints>();

            ICleaningPoints cleanByPoints = new CleanByPoints(4);
            ICleaningPoints cleanBySize = new CleanBySize(2000);

            Hybrid2.Add(cleanBySize);
            Hybrid2.Add(cleanByPoints);

            ICleaningPoints CleanByOneTerm = new HybridCleanOneTerm(Hybrid2);
            CleanByOneTerm.StartClean(newChain.Points);
            Assert.AreEqual(6, newChain.ShowRestorePoints().Count);
        }
    }
   
}