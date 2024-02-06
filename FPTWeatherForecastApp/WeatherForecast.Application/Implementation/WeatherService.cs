using FPTWeatherForecastApp.WeatherForecast.Application.DTO;
using FPTWeatherForecastApp.WeatherForecast.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FPTWeatherForecastApp.WeatherForecast.Application.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        public WeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<WeatherDataDTO> GetWeatherDataAsync(string location)
        {
            string apiKey = _configuration["WeatherstackApiKey"];
            string apiUrl = $"http://api.weatherstack.com/current?access_key={apiKey}&query={location}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherDataDTO>(json);
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
            }
        }
    }
}
