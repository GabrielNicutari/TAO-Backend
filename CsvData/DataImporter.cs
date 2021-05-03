using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TAO_Backend.Models;

namespace TAO_Backend.CsvData
{
    public class DataImporter
    {
        public DataImporter()
        {
        }

        public void Import()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\csv_files\Daily_readings_clean.csv"));

            System.Diagnostics.Debug.WriteLine(path);

            using (var streamReader = new StreamReader(newPath)) 

            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" };
                using (var csvReader = new CsvReader(streamReader, config))
                {
                    // DATA IMPORTING
                    csvReader.Context.RegisterClassMap<DailyReadingMapper>();
                    var records =
                        csvReader
                            .GetRecords<DailyReading>()
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
                                record.Energy = energyValue.ToString();
                            }

                            if (record.Volume.Contains(",")) 
                            {
                                var volumeValue = Double.Parse(record.Volume, new CultureInfo(CultureInfo.CurrentCulture.Name)
                                {
                                    NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "," }
                                });
                                volumeValue *= 1000;
                                record.Volume = volumeValue.ToString();
                            }
                        });

                    for (int i = 0; i < 20; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(records[i].Volume);
                    }
                }
            }
        }
    }
}
