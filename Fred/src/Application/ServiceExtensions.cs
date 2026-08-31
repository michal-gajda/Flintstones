namespace Fred.Application;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
