using Core.Cqrs;
using Domain.Users.Dto;
using Domain.Users.Repositories;

namespace Domain.Users.Queries;

public record UsersQuery() : IQuery<IEnumerable<UserDto>>;

internal class UsersQueryHandler(IUserRepository _userRepository) : IQueryHandler<UsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(UsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.FindAsync(cancellationToken);
        var userDtos = users.Select(
            user => new UserDto(
                user.Id, 
                user.UserName, 
                user.Email, 
                user.Role))
            .ToList();
        return userDtos;
    }
}