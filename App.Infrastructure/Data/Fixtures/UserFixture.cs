using App.Domain.Model.Country;
using App.Domain.Model.User;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Fixtures;

public static class UserFixture
{
    public static void SeedUsers(ModelBuilder modelBuilder, List<CountryEntity> countries)
    {
        var users = new List<UserEntity>();
        
        // US User
        var usUser = new UserEntity
        {
            Id = 1,
            UserName = "johndoe",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Country = countries.First(c => c.CountryCode == "US")
        };
        users.Add(usUser);

        // JP User
        var jpUser = new UserEntity
        {
            Id = 2,
            UserName = "taro.yamada",
            FirstName = "Taro",
            LastName = "Yamada",
            Email = "taro.yamada@example.com",
            Country = countries.First(c => c.CountryCode == "JP")
        };
        users.Add(jpUser);

        // DE User
        var deUser = new UserEntity
        {
            Id = 3,
            UserName = "hans.mueller",
            FirstName = "Hans",
            LastName = "Müller",
            Email = "hans.mueller@example.com",
            Country = countries.First(c => c.CountryCode == "DE")
        };
        users.Add(deUser);

        // GB User
        var gbUser = new UserEntity
        {
            Id = 4,
            UserName = "emily.smith",
            FirstName = "Emily",
            LastName = "Smith",
            Email = "emily.smith@example.com",
            Country = countries.First(c => c.CountryCode == "GB")
        };
        users.Add(gbUser);

        // FR User
        var frUser = new UserEntity
        {
            Id = 5,
            UserName = "sophie.martin",
            FirstName = "Sophie",
            LastName = "Martin",
            Email = "sophie.martin@example.com",
            Country = countries.First(c => c.CountryCode == "FR")
        };
        users.Add(frUser);

        // First, seed the users with CountryId
        var userData = users.Select(u => new
        {
            u.Id,
            u.UserName,
            u.FirstName,
            u.LastName,
            u.Email,
            CountryId = u.Country.Id
        }).ToList();

        // Seed the users
        modelBuilder.Entity<UserEntity>().HasData(userData);
    }
}
