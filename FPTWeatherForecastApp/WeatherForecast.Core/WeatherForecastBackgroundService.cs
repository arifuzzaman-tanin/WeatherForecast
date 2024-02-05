using FPTWeatherForecastApp.WeatherForecast.Application.DTO;
using FPTWeatherForecastApp.WeatherForecast.Application.Implementation;
using FPTWeatherForecastApp.WeatherForecast.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FPTWeatherForecastApp.WeatherForecast.Core
{

    public class WeatherForecastBackgroundService : BackgroundService
    {
        private readonly ILogger<WeatherService> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastBackgroundService(IWeatherService weatherService, ILogger<WeatherService> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("\n\nEnter the location (Example: M4V 2B6 or Toronto)");
                string zipcode = Console.ReadLine();

                WeatherDataDTO weatherData = await _weatherService.GetWeatherDataAsync(zipcode);
                DisplayWeatherInformation(weatherData);
            }
        }

        private void DisplayWeatherInformation(WeatherDataDTO weatherData)
        {
            if (weatherData.Current != null)
            {
                if (weatherData.Location != null)
                {
                    Console.WriteLine($"\nWeather forecast for {weatherData.Location.Name}, {weatherData.Location.Country}:");
                }
                Console.WriteLine($"Should I go outside? {(weatherData.Current.Precip == 0 ? "Yes" : "No, it's raining")}");
                Console.WriteLine($"Should I wear sunscreen? {(weatherData.Current.UvIndex > 3 ? "Yes" : "No")}");
                Console.WriteLine($"Can I fly my kite? {(weatherData.Current.Precip == 0 && weatherData.Current.WindSpeed > 15 ? "Yes" : "No")}");
            }
            else
            {
                Console.WriteLine("Something wrong, please try again");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stop : {DateTime.Now}");
            return base.StopAsync(cancellationToken);
        }
    }
}
