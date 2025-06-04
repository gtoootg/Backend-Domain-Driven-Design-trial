using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Domain.Model.User;
using MediatR;

namespace App.Application.Queries.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserReadModel>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserReadModel>>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetAllUsersQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<IEnumerable<UserReadModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userReadRepository.GetAllAsync();
        }
    }
}
