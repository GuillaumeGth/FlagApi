using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FlagApi;

namespace FlagApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((constext, builder) => {
                        builder.ClearProviders();
                        builder.AddProvider(new LoggerProvider());
                    });
                    webBuilder.UseStartup<Startup>();                    
                });
    }
}
