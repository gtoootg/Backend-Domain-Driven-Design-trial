using App.Domain.Model.Country;
using App.Domain.Model.User;
using App.Infrastructure.Data.Fixtures;
using App.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new CountryEntityMap());
            modelBuilder.ApplyConfiguration(new UserEntityMap());
        }
    }
}