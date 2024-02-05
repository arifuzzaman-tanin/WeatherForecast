using FPTWeatherForecastApp.WeatherForecast.Application.Implementation;
using FPTWeatherForecastApp.WeatherForecast.Application.Interfaces;
using FPTWeatherForecastApp.WeatherForecast.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var builder = new HostBuilder()
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.SetBasePath(Directory.GetCurrentDirectory());
               config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
               if (args != null) config.AddCommandLine(args);
           })
           .ConfigureServices((hostingContext, services) =>
           {
               services.AddHostedService<WeatherForecastBackgroundService>();

               services.AddSingleton<IWeatherService, WeatherService>(provider =>
               {
                   var configuration = provider.GetRequiredService<IConfiguration>();
                   return new WeatherService(configuration);
               });
           });

            await builder.RunConsoleAsync();
        }
        catch
        {
            Console.WriteLine("Program throw exception");
        }
    }
}