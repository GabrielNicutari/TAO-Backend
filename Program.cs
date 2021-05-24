using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using TAO_Backend.CsvData;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TAO_Backend.Models;
using TAO_Backend.Services;

namespace TAO_Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // TranslationService translationService = new TranslationService();
            // string[] words = new string[] { "hello", "there", "play with me tonight!" };
            // string[] result = translationService.TranslateWords(words, "el");
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DBContext>();
                Thread thread = new Thread(() =>
                {
                    var fileWatcher = new FileWatcher(context);
                    fileWatcher.Start();
                });
                thread.Start();
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
