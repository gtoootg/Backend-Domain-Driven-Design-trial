using App.Domain.Model.User;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<UserEntity> Users { get; set; }
    }
}