using App.Domain.Model.User;
using Microsoft.EntityFrameworkCore;

namespace YourApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // コンストラクタ（DIでDbContextOptionsを受け取る）
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // テーブル（DbSet）はまだないので空です。
        // 例:
        public DbSet<UserEntity> Users { get; set; }
    }
}