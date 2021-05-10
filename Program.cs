using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using TAO_Backend.CsvData;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TAO_Backend.Models;

namespace TAO_Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // start a separate thread which is responsible for watching for changes in the csv file and
            // importing the data when the change occurs.
            Thread thread = new Thread(() => 
            {
                var fileWatcher = new FileWatcher();
                fileWatcher.Start();
            });
            thread.Start();
            // then we start the program
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DBContext>();
                // await context.Database.MigrateAsync();
                // await Seed.SeedData(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration.");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
