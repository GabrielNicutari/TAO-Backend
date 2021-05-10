using System;
using System.IO;
using System.Threading;

namespace TAO_Backend.CsvData
{
    public class FileWatcher
    {
        public void Start()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\csv_files"));
            Thread.Sleep(10000);
            Console.WriteLine(newPath);
            using var watcher = new FileSystemWatcher(newPath);
            
                watcher.NotifyFilter = NotifyFilters.Attributes
                | NotifyFilters.CreationTime
                | NotifyFilters.DirectoryName
                | NotifyFilters.FileName
                | NotifyFilters.LastAccess
                | NotifyFilters.LastWrite
                | NotifyFilters.Security
                | NotifyFilters.Size;
        
                //watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                //watcher.Deleted += OnDeleted;
                //watcher.Renamed += OnRenamed;
                //watcher.Error += OnError;
        
                watcher.Filter = "*.csv";
                watcher.IncludeSubdirectories = true;
                watcher.EnableRaisingEvents = true;
                while (true)
                {
                }
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine($"file name= {e.Name}");
                string fileName = e.Name;
                if (fileName.Equals("Daily_readings_clean.csv"))
                {
                    var dataImporter = new DataImporter();
                    // we need to wait for the OS to copy the file to our folder. 
                    // if we try to run the Import method while the copying is performed,
                    // then we get an Exception that the file is being used by other process.
                    Thread.Sleep(3000);
                    dataImporter.Import();
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}