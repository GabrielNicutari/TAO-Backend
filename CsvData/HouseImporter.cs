using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TAO_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using TAO_Backend.Models;

namespace TAO_Backend.CsvData
{
    public class HouseImporter
    {
        private readonly DBContext _context;

        public HouseImporter() { }

        public HouseImporter(DBContext context)
        {
            _context = context;
        }
        
        public async void Import()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\csv_files\Houses.csv"));

            System.Diagnostics.Debug.WriteLine(path);
            using var streamReader = new StreamReader(newPath);
            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" };
            using var csvReader = new CsvReader(streamReader, config);
            // DATA IMPORTING
            csvReader.Context.RegisterClassMap<HouseMapper>();
            var records =
                csvReader
                    .GetRecords<HouseCsv>()
                    .ToList();
            
            // Console.WriteLine("records count= " + records.Count);
            // Console.WriteLine("record= " + records[2].Address);

            Console.WriteLine(await _context.Houses.FindAsync(10));
            var houseList = new List<House>();
            records.ForEach(record =>
            {
                House house = new House(
                    Convert.ToInt32(record.HouseId),
                    record.Address,
                    record.Zip,
                    Convert.ToInt32(record.Area),
                    Convert.ToInt32(record.YearBuilt));
                houseList.Add(house);
            });
            await _context.AddRangeAsync(houseList);
            await _context.SaveChangesAsync();
            // var a = _context.Houses.First();
            // Console.WriteLine(a.Address);

            // for (int i = 0; i < 20; i++)
            // {
            //     House house = new House(
            //         records[i].Address,
            //         records[i].Zip,
            //         Convert.ToInt32(records[i].Area),
            //         Convert.ToInt32(records[i].YearBuilt));
            //     _context.Houses.Add(house);
            // }

            // for (int i = 1; i < 10; i++)
            // {
            //     var house = new House();
            //     house.Id = i;
            //     var result = _context.Houses.Find(1);
            //     Console.WriteLine(house.Address);
            // }

            // var result = _context.Houses.Find();
            // if (result != null)
            // {
            //     Console.WriteLine(result.GetType());
            // }
            // else
            // {
            //     Console.WriteLine("null...");
            // }

        }
    }
}
