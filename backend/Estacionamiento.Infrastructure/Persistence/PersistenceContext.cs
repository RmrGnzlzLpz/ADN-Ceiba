using System;
using System.Threading.Tasks;
using Estacionamiento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Estacionamiento.Infrastructure.Persistence
{
    public class PersistenceContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleHistory> VehicleHistory { get; set; }

        private readonly IConfiguration _config;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            modelBuilder.HasDefaultSchema(_config.GetValue<string>("SchemaName"));

            modelBuilder.Entity<Vehicle>().Property(x => x.License).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
