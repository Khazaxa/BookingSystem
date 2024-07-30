using Core.Cqrs;
using Core.Helpers;
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
        
        var password = input.Password;
        var passwordSalt = PasswordHelper.GenerateSalt();
        var passwordHash = PasswordHelper.HashPassword(password);

        var user = new User(
            input.Name,
            input.Email,
            passwordHash,
            passwordSalt,
            input.Role
        );

        var addedUser = await _userRepository.AddAsync(user, cancellationToken);

        return addedUser.Id;
    }
}