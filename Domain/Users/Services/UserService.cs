using Core.Exceptions;
using Domain.Users.Enums;
using Domain.Users.Repositories;

namespace Domain.Users.Services;

internal class UserService(IUserRepository _userRepository) : IUserService
{
    public async Task ValidateIfEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByEmailAsync(email, cancellationToken);
        if (user is not null)
            throw new DomainException("User with provided email already exists", (int)UserErrorCode.EmailInUse);
    }
}