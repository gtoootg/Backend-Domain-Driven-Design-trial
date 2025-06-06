using App.Domain.Model.Country;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Fixtures;

public static class CountryFixture
{
    public static List<CountryEntity> GetCountries()
    {
        return new List<CountryEntity>
        {
            new()
            {
                Id = 1,
                Name = "United States",
                CountryCode = "US"
            },
            new()
            {
                Id = 2,
                Name = "Japan",
                CountryCode = "JP"
            },
            new()
            {
                Id = 3,
                Name = "Germany",
                CountryCode = "DE"
            },
            new()
            {
                Id = 4,
                Name = "United Kingdom",
                CountryCode = "GB"
            },
            new()
            {
                Id = 5,
                Name = "France",
                CountryCode = "FR"
            }
        };

    }

    
    public static void SeedCountries(ModelBuilder modelBuilder)
    {
        var countries = GetCountries();
        modelBuilder.Entity<CountryEntity>().HasData(countries);
    }
}
