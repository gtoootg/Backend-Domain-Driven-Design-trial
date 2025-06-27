using App.Domain.Model.Country;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Fixtures;

public class CountryFixture : IFixture
{
    public async Task SeedAsync(AppDbContext context)
    {
        if (await context.Countries.AnyAsync())
            return;

        var countries = new List<CountryEntity>
        {
            new() { Id = 1, Name = "United States", CountryCode = "US", CreatedAt =  DateTime.UtcNow },
            new() { Id = 2, Name = "Japan", CountryCode = "JP", CreatedAt =  DateTime.UtcNow },
            new() { Id = 3, Name = "Germany", CountryCode = "DE" , CreatedAt =  DateTime.UtcNow},
            new() { Id = 4, Name = "United Kingdom", CountryCode = "GB" , CreatedAt =  DateTime.UtcNow},
            new() { Id = 5, Name = "France", CountryCode = "FR", CreatedAt =  DateTime.UtcNow }
        };

        await context.Countries.AddRangeAsync(countries);
        await context.SaveChangesAsync();
    }
}
