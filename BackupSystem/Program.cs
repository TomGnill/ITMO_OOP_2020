using System;
using System.Collections.Generic;
using System.Threading;

namespace BackupSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager newSys = new FileManager();

            DateTime date1 = new DateTime(2019, 7, 20, 18, 30, 25);

            AbstractFile file1 = new AbstractFile("Test1", 100, date1);
            AbstractFile file2 = new AbstractFile("Test2", 100, date1);

            newSys.AddFile(file1);
            newSys.AddFile(file2);

            List<AbstractFile> list = newSys.EndEditing();
            RestoreSystem system = new RestoreSystem();
            StorageAlgorithms algorithms = new StorageAlgorithms();

            var point1 =  algorithms.GeneralBackup(list, Type.Full);
            system.AddPoint(point1);
            var point2 =  algorithms.SeparateBackup(list, Type.Full);
            system.AddPoint(point2);
            ICreateRestorePoint create = new CreateGeneralBackup(system); //через интерфейс.
            create.CreateBackup(list, Type.Full);
            system.ShowRestorePoints();
            ICleaningPoints Clean = new CleanByPoints(1);
            Clean.Clean(system.Points);
            system.ShowRestorePoints();

        }
    }
}
/*
 public BackupJob(
    Backup backup,
    IFileCopyCreateAlgorithm fileCopyCreateAlgorithm,
    IPointCreateAlgorithm pointCreateAlgorithm,
    IPointDeleteAlgorithm pointDeleteAlgorithm)
{
    _backup = backup;
    _fileCopyCreateAlgorithm = fileCopyCreateAlgorithm;
    _pointCreateAlgorithm = pointCreateAlgorithm;
    _pointDeleteAlgorithm = pointDeleteAlgorithm;
}

public void Run()
{
    //_fileCopyCreateAlgorithm.CreateFor(_backup);
    //_pointCreateAlgorithm.CreateFor(_backup);
    //_pointDeleteAlgorithm.RemoveFrom(_backup);
}
 */
