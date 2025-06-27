using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Model.User;

public interface IUserReadRepository
{
    Task<UserReadModel?> GetByIdAsync(int id);
    Task<UserReadModel?> GetByEmailAsync(string email);
    Task<IEnumerable<UserReadModel>> GetAllAsync();
}
