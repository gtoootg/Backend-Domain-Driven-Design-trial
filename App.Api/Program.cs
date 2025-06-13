using Microsoft.EntityFrameworkCore;
using App.Domain.Model.User;
using App.Infrastructure.Repositories;
using App.Api.Endpoints;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Fixtures;

var builder = WebApplication.CreateBuilder(args);

var migrate = args.Contains("--migrate");
var seed = args.Contains("--seed");
var resetDb = args.Contains("--reset-db");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(App.Application.Queries.Users.GetAllUsersQuery).Assembly));

builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        
        // データベースをリセットするオプション
        if (resetDb)
        {
            Console.WriteLine("Resetting database...");
            await context.Database.EnsureDeletedAsync();
        }
        
        // マイグレーションを実行
        if (migrate)
        {
            Console.WriteLine("Applying migrations...");
            await context.Database.MigrateAsync();
        }
        
        // シードデータを実行
        if (seed)
        {
            Console.WriteLine("Seeding database...");
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            await FixtureManager.SeedAllAsync(context);
            Console.WriteLine("Database seeding completed.");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        throw;
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map endpoints
app.MapUserEndpoints();
app.MapLoginEndpoints();

app.Run();