namespace Fred.Application.Queries;

using Fred.Application.QueryResults;

public sealed record class GetWeatherForecasts : IRequest<IEnumerable<WeatherForecast>>
{
}
