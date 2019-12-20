namespace BE.API
{
  using System.IO;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using Serilog;
  using Serilog.Events;

  public class Program
  {
    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File("./Log/log.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateLogger();

      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureLogging(logging =>
            {
              logging.ClearProviders();
              logging.AddConsole();
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
              config.SetBasePath(Directory.GetCurrentDirectory());
              config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
              config.AddEnvironmentVariables();
              config.AddCommandLine(args);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
