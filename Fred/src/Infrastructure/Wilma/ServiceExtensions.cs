namespace Fred.Infrastructure.Wilma;

using Fred.Infrastructure.Wilma.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddWilma(IConfiguration configuration)
        {
            var options = configuration.GetSection(WilmaOptions.SectionName).Get<WilmaOptions>();

            options ??= new WilmaOptions();

            services.AddRefitClient<IWilmaService>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(options.Address))
                .AddPolicyHandler((serviceProvider, request) =>
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<IWilmaService>>();

                    return HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            retryCount: 3,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (outcome, timespan, retryCount, context) =>
                            {
                                logger.LogWarning(
                                    "Request failed with {StatusCode}. Retrying attempt {RetryCount} after {Delay}ms. URL: {Url}",
                                    outcome.Result?.StatusCode,
                                    retryCount,
                                    timespan.TotalMilliseconds,
                                    request.RequestUri);
                            });
                });

            return services;
        }
    }
}
