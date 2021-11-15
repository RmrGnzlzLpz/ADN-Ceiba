using System;
using System.Linq;
using Estacionamiento.Domain.Ports;
using Estacionamiento.Domain.Services;
using Estacionamiento.Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Estacionamiento.Infrastructure.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IParkingConfiguration, ParkingConfiguration>();

            var _services = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName!.Contains("Domain", StringComparison.InvariantCulture))
                .SelectMany(s => s.GetTypes())
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

            foreach (var _service in _services)
            {
                services.AddTransient(_service);
            }

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
