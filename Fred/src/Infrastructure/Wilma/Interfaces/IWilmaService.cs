namespace Fred.Infrastructure.Wilma.Interfaces;

using Fred.Infrastructure.Wilma.Models;
using Refit;

internal interface IWilmaService
{
    [Get("/weatherforecast/")]
    Task<List<WeatherForecast>> GetWeatherForecastAsync(CancellationToken cancellationToken = default);
}
