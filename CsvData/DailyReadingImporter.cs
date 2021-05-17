using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TAO_Backend.Models;

namespace TAO_Backend.CsvData
{
    public class DailyReadingImporter
    {
        private readonly DBContext _context;

        public DailyReadingImporter()
        {
        }

        public DailyReadingImporter(DBContext context)
        {
            _context = context;
        }
        
        public async void Import()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\csv_files\Daily_readings_clean.csv"));

            System.Diagnostics.Debug.WriteLine(path);

            using var streamReader = new StreamReader(newPath);
            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" };
            using var csvReader = new CsvReader(streamReader, config);
            // DATA IMPORTING
            csvReader.Context.RegisterClassMap<DailyReadingMapper>();
            var records =
                csvReader
                    .GetRecords<DailyReadingCsv>()
                    .ToList();


            // DATA CLEANING
            records.ForEach(record =>
            {
                if (record.Energy.Contains(",") || record.Energy.Length < 4)
                {
                    var energyValue = Double.Parse(record.Energy, new CultureInfo(CultureInfo.CurrentCulture.Name)
                    {
                        NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "," }
                    });
                    energyValue *= 1000;
                    record.Energy = energyValue.ToString(CultureInfo.InvariantCulture);
                }

                if (record.Volume.Contains(",")) 
                {
                    var volumeValue = Double.Parse(record.Volume, new CultureInfo(CultureInfo.CurrentCulture.Name)
                    {
                        NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "," }
                    });
                    volumeValue *= 1000;
                    record.Volume = volumeValue.ToString(CultureInfo.InvariantCulture);
                }
            });
            
            var dailyReadingsList = new List<DailyReading>();
            records.ForEach(record =>
            {
                DailyReading dailyReading = new DailyReading(
                    Convert.ToDateTime(record.Timestamp), 
                    Convert.ToDouble(record.Energy), 
                    Convert.ToDouble(record.Volume),
                    Convert.ToInt32(record.HourCounter),
                    Convert.ToDouble(record.TempForward),
                    Convert.ToDouble(record.TempReturn), 
                    Convert.ToDouble(record.Power),
                    Convert.ToInt32(record.Flow),
                    Convert.ToInt32(record.HouseId)
                );
                dailyReadingsList.Add(dailyReading);
            });
            await _context.AddRangeAsync(dailyReadingsList);
            await _context.SaveChangesAsync();
            Console.WriteLine("\n\nData importing completed.");
        }
    }
}
