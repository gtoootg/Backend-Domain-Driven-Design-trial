namespace App.Infrastructure.Data.Fixtures;

public interface IFixture
{
    Task SeedAsync(AppDbContext context);
}