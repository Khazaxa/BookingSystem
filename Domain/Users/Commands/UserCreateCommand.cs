using System.Security.Cryptography;
using System.Text;
using Core.Cqrs;
using Domain.Users.Dto;
using Domain.Users.Entities;
using Domain.Users.Repositories;
using Domain.Users.Services;

namespace Domain.Users.Commands;

public record UserCreateCommand(UserParams Input) : ICommand<int>;

internal class UserCreateCommandHandler(
    IUserRepository _userRepository,
    IUserService _userService
) : ICommandHandler<UserCreateCommand, int>
{
    public async Task<int> Handle(UserCreateCommand command, CancellationToken cancellationToken)
    {
        var input = command.Input;
        await _userService.ValidateIfEmailExistsAsync(input.Email, cancellationToken);

        using var hmac = new HMACSHA512();
        var user = new User(
            input.UserName,
            input.Email,
            hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)),
            hmac.Key,
            input.Role
        );

        var addedUser = await _userRepository.AddAsync(user, cancellationToken);

        return addedUser.Id;
    }
}