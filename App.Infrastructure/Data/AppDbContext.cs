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
            modelBuilder.ApplyConfiguration(new UserEntityMap());

            // Configure CountryEntity
            // modelBuilder.Entity<CountryEntity>(entity =>
            // {
            //     entity.ToTable("Countries");
            //     entity.HasKey(c => c.Id);
            //     entity.Property(c => c.CountryCode)
            //         .IsRequired()
            //         .HasMaxLength(10);
            //     entity.HasIndex(c => c.CountryCode)
            //         .IsUnique();
            //     entity.Property(c => c.Name)
            //         .IsRequired()
            //         .HasMaxLength(100);
            // });

            // Seed countries first
            var countries = CountryFixture.GetCountries();
            modelBuilder.Entity<CountryEntity>().HasData(countries);

            // Then seed users with the country relationships
            UserFixture.SeedUsers(modelBuilder, countries);

            // Ensure the database is created with the latest model
            Database.EnsureCreated();
        }
    }
}