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
        private readonly string _filePath; // path to the file to be imported
        public DailyReadingImporter(DBContext context, string filePath)
        {
            _context = context;
            _filePath = filePath;
        }
        public async void ImportAndSave()
        {
            List<DailyReadingCsv> records = ImportDataFromCsv();
            records = RemoveCommasAndMultipleByThousand(records);
            List<DailyReading> dailyReadingsList = mapDailyReadingCsvToDailyReading(records);
            // // for testing
            // for (int i = 0; i < 5; i++)
            // {
            //     Console.WriteLine($"date= {dailyReadingsList[i].Timestamp}, house id= {dailyReadingsList[i].HouseReadingId}");
            // }
            await _context.AddRangeAsync(dailyReadingsList);
            await _context.SaveChangesAsync();
            Console.WriteLine("\n\nData importing completed.");
        }
        private List<DailyReadingCsv> ImportDataFromCsv() 
        {
            using var streamReader = new StreamReader(_filePath);
            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" };
            using var csvReader = new CsvReader(streamReader, config);
            csvReader.Context.RegisterClassMap<DailyReadingMapper>();
            return csvReader.GetRecords<DailyReadingCsv>().ToList();
        }
        private List<DailyReadingCsv> RemoveCommasAndMultipleByThousand(List<DailyReadingCsv> records)
        {
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
            return records;
        }
        private List<DailyReading> mapDailyReadingCsvToDailyReading(List<DailyReadingCsv> records)
        {
            List<DailyReading> dailyReadingsList = new List<DailyReading>();
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
            return dailyReadingsList;
        }
    }
}
