using System;
using System.Linq;
using System.Reflection;
using Estacionamiento.Api.Filters;
using Estacionamiento.Infrastructure.Extensions;
using Estacionamiento.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Estacionamiento.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        private readonly string _anyOriginPolicy = "any-policy";
        private readonly string _Policy = "policy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var acceptedUrls = Configuration.GetValue<string>("AllowedHosts").Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy(_anyOriginPolicy, opt => opt.SetIsOriginAllowed(x => true).AllowAnyMethod().AllowAnyHeader());
            });
            services.AddCors(options =>
            {
                options.AddPolicy(_Policy, opt => opt.WithOrigins(acceptedUrls).AllowAnyMethod().AllowAnyHeader());
            });

            var applicationAssemblyName = typeof(Startup).Assembly.GetReferencedAssemblies()
                .FirstOrDefault(x => x.Name.Equals("Estacionamiento.Application", StringComparison.InvariantCulture));
            services.AddAutoMapper(Assembly.Load(applicationAssemblyName.FullName));
            services.AddMediatR(Assembly.Load("Estacionamiento.Application"), typeof(Startup).Assembly);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AppExceptionFilterAttribute));
            });

            services.AddDbContext<PersistenceContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("database"), sqlopts =>
                {
                    sqlopts.MigrationsHistoryTable("_MigrationHistory", Configuration.GetValue<string>("SchemaName"));
                    sqlopts.MigrationsAssembly("Estacionamiento.Api");
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Rest - Parking",
                    Version = "v1"
                });
            });

            services.AddRepositories().AddDomainServices();

            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking v1"));
            }

            if (env.IsDevelopment())
            {
                app.UseCors(_anyOriginPolicy);
            }
            else
            {
                app.UseCors(_Policy);
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();
        }
    }
}
