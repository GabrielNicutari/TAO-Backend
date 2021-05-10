using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using TAO_Backend.CsvData;

namespace TAO_Backend
{
    public class Program
    {
        public static void Main(string[] args)
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
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
