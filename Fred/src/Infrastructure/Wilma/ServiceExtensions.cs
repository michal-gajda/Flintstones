namespace Fred.Infrastructure.Wilma;

using Fred.Infrastructure.Wilma.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(options.Address));

            return services;
        }
    }
}
