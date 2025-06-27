using App.Domain.Model.User;
using MediatR;

namespace App.Application.Queries.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserReadModel>>
    {
    }

    public class GetAllUsersQueryHandler(IUserReadRepository userReadRepository) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserReadModel>>
    {
        public async Task<IEnumerable<UserReadModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await userReadRepository.GetAllAsync();
        }
    }
}
