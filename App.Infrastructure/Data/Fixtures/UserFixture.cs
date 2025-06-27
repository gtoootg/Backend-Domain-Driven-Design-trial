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

        var user1 = new UserEntity { UserName = "john.doe", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Country = us, CreatedAt =  DateTime.UtcNow }; 
        user1.SetPassword("password");
        var user2 = new UserEntity { UserName = "jane.doe", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", Country = us, CreatedAt =  DateTime.UtcNow }; 
        user2.SetPassword("password");
        var user3 = new UserEntity { UserName = "bob.smith", FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", Country = us, CreatedAt =  DateTime.UtcNow }; 
        user3.SetPassword("password");
        var user4 = new UserEntity { UserName = "alice.jones", FirstName = "Alice", LastName = "Jones", Email = "alice.jones@example.com", Country = us, CreatedAt =  DateTime.UtcNow }; 
        user4.SetPassword("password");
        var user5 = new UserEntity { UserName = "mike.wilson", FirstName = "Mike", LastName = "Wilson", Email = "mike.wilson@example.com", Country = us, CreatedAt =  DateTime.UtcNow }; 
        user5.SetPassword("password");
        var user6 = new UserEntity { UserName = "sarah.brown", FirstName = "Sarah", LastName = "Brown", Email = "sarah.brown@example.com", Country = us, CreatedAt =  DateTime.UtcNow }; 
        user6.SetPassword("password");
        
        var users = new List<UserEntity>
        {
            user1,
            user2,
            user3,
            user4,
            user5,
            user6
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}
