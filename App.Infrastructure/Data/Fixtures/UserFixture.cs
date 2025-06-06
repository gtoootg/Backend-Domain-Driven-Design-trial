using App.Domain.Model.Country;
using App.Domain.Model.User;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Fixtures;

public class UserFixture : IFixture
{
    public async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        // Get the countries that were just seeded
        var us = await context.Countries.FirstAsync(c => c.CountryCode == "US");
        var jp = await context.Countries.FirstAsync(c => c.CountryCode == "JP");
        var de = await context.Countries.FirstAsync(c => c.CountryCode == "DE");
        var gb = await context.Countries.FirstAsync(c => c.CountryCode == "GB");
        var fr = await context.Countries.FirstAsync(c => c.CountryCode == "FR");

        var users = new List<UserEntity>
        {
            new() { UserName = "suzuki.ichiro", FirstName = "Ichiro", LastName = "Suzuki", Email = "suzuki.ichiro@example.com", Country = us }, // NOTE: This is a Japanese name, but we're using US as the default country code for simplicity", LastName = "Doe", Email = "john.doe@example.com", Country = us },
            new() { UserName = "taro.yamada", FirstName = "Taro", LastName = "Yamada", Email = "taro.yamada@example.com", Country = jp },
            new() { UserName = "hans.mueller", FirstName = "Hans", LastName = "Müller", Email = "hans.mueller@example.com", Country = de },
            new() { UserName = "emily.smith", FirstName = "Emily", LastName = "Smith", Email = "emily.smith@example.com", Country = gb },
            new() { UserName = "sophie.martin", FirstName = "Sophie", LastName = "Martin", Email = "sophie.martin@example.com", Country = fr }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}
