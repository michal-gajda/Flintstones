namespace Fred.Application.QueryResults;

public sealed record class WeatherForecast
{
    public required DateTime Date { get; init; }
    public required int TemperatureC { get; init; }
    public required int TemperatureF { get; init; }
    public required string Summary { get; init; } = string.Empty;
}
