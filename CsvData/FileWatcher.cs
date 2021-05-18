using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using TAO_Backend.Models;

namespace TAO_Backend.CsvData
{
    // the FileWatcher class runs in a separate thread in the background and listens to changes in
    // csv_files directory of this project. When a new .csv file with a new name is created, then
    // it calls a method that imports the daily readings data from the newly created .csv
    public class FileWatcher
    {
        private readonly DBContext _context;
        private readonly string _path; // path to the folder with the csv files
        private List<string> _fileNames; // file names in the csv_files folder
        public FileWatcher(DBContext context)
        {
            _context = context;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            _path = Path.GetFullPath(Path.Combine(path, @"..\..\..\csv_files"));
            var filePaths = Directory.GetFiles(_path, "*.csv",SearchOption.TopDirectoryOnly);
            List<string> fileNames = new List<string>();
            foreach (var filePath in filePaths)
            {
                fileNames.Add(Path.GetFileName(filePath));
            }
            _fileNames = fileNames;
        }
        public void Start()
        {
            using var watcher = new FileSystemWatcher(_path)
            {
                NotifyFilter = NotifyFilters.Attributes
                               | NotifyFilters.CreationTime
                               | NotifyFilters.DirectoryName
                               | NotifyFilters.FileName
                               | NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.Security
                               | NotifyFilters.Size
            };

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
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine($"\n***\nFile Created = {e.Name}\n***\n");
                var fileName = e.Name;
                // it gets triggered only if the new file does not already exist in the csv_files folder
                if (!_fileNames.Contains(fileName))
                {
                    // update the file names property 
                    _fileNames.Add(fileName);
                    // it needs to wait for the OS to copy the file to our folder. 
                    // if it tries to run the Import method while the copying is performed,
                    // then we get an Exception that the file is being used by other process.
                    Thread.Sleep(3000);
                    DailyReadingImporter dailyReadingImporter = new DailyReadingImporter(_context, $"{_path}\\{fileName}");
                    dailyReadingImporter.ImportAndSave();
                }
                else
                {
                    Console.WriteLine("The file already exists.");
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}