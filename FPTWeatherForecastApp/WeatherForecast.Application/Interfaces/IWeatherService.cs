using FPTWeatherForecastApp.WeatherForecast.Application.DTO;

namespace FPTWeatherForecastApp.WeatherForecast.Application.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherDataDTO> GetWeatherDataAsync(string location);
    }
}
