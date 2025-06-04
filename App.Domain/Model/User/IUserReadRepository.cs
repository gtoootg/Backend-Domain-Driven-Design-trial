using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Model.User;

public interface IUserReadRepository
{
    Task<UserReadModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserReadModel>> GetAllAsync();
}
