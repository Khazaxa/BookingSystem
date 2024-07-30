using Core.Cqrs;
using Domain.Users.Dto;
using Domain.Users.Repositories;

namespace Domain.Users.Queries;

public record UsersQuery() : IQuery<IEnumerable<UserDto>>;

internal class UsersQueryHandler(IUserRepository _userRepository) : IQueryHandler<UsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(UsersQuery request, CancellationToken cancellationToken)
    {
        return null; //await _userRepository
        // .GetAll()
        // ), cancellationToken);
    }
}