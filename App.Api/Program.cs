using System.Text;
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using App.Domain.Model.User;
using App.Infrastructure.Repositories;
using App.Api.Endpoints;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Fixtures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using App.Infrastructure.Services;
using App.Domain.Services;

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

var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDistributedMemoryCache();

  builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });
        
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = new AmazonS3Config
    {
        ServiceURL = builder.Configuration["S3:ServiceUrl"],
        ForcePathStyle = true
    };

    return new AmazonS3Client(
            builder.Configuration["S3:AccessKey"],
            builder.Configuration["S3:SecretKey"],
            config
        );
});

// Register domain-level service for file uploads
builder.Services.AddScoped<IFileUploader>(sp =>
{
    var s3 = sp.GetRequiredService<IAmazonS3>();
    var bucketName = builder.Configuration["S3:BucketName"] ?? "uploads";
    return new S3FileUploader(s3, bucketName);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        if (resetDb)
        {
            Console.WriteLine("Resetting database...");
            await context.Database.EnsureDeletedAsync();
        }
        
        if (migrate)
        {
            Console.WriteLine("Applying migrations...");
            await context.Database.MigrateAsync();
        }
        
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

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

// Map endpoints
app.MapUserEndpoints();
app.MapLoginEndpoints();
app.MapFileEndpoints();

app.Run();