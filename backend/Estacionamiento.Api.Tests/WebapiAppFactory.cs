using Estacionamiento.Infrastructure.Extensions;
using Estacionamiento.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Estacionamiento.Api.Tests
{
    public class WebapiAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {

            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>().ConfigureServices(services =>
                    {
                        var dbCtxOpts = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PersistenceContext>));

                        if (dbCtxOpts != null)
                        {
                            services.Remove(dbCtxOpts);
                        }

                        services.AddDbContext<PersistenceContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
        }
    }
}
