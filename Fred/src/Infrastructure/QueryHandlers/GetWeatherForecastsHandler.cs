namespace Fred.Infrastructure.QueryHandlers;

using Fred.Application.Queries;
using Fred.Application.QueryResults;
using Fred.Infrastructure.Wilma.Interfaces;

internal sealed class GetWeatherForecastsHandler(IWilmaService service) : IRequestHandler<GetWeatherForecasts, IEnumerable<WeatherForecast>>
{
    public async Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecasts request, CancellationToken cancellationToken)
    {
        var models = await service.GetWeatherForecastAsync(cancellationToken);

        return models.Select(model => new WeatherForecast
        {
            Date = model.Date,
            TemperatureC = model.TemperatureC,
            TemperatureF = model.TemperatureF,
            Summary = model.Summary
        });
    }
}
