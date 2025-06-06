namespace App.Infrastructure.Data.Fixtures;

public static class FixtureManager
{
    public static async Task SeedAllAsync(AppDbContext context)
    {
        var fixtures = new List<IFixture>
        {
            new CountryFixture(),
            new UserFixture()
        };

        foreach (var fixture in fixtures)
        {
            await fixture.SeedAsync(context);
        }
    }
}