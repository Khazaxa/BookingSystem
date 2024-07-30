using Core.Exceptions;
using Domain.Users.Enums;
using Domain.Users.Repositories;

namespace Domain.Users.Services;

internal class UserService(IUserRepository _userRepository) : IUserService
{
    public void ValidateIfEmailExists(string email)
    {
        var user = _userRepository.FindByEmail(email);
        if (user is not null)
            throw new DomainException("Email already exists", (int)UserErrorCode.EmailInUse);
    }
}