using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Model.User;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly AppDbContext _context;

        public UserReadRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserReadModel?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return entity != null ? UserReadModel.FromEntity(entity) : null;
        }

        public async Task<IEnumerable<UserReadModel>> GetAllAsync()
        {
            var entities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(UserReadModel.FromEntity);
        }
    }
}
