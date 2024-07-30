using Domain.Users.Enums;

namespace Domain.Users.Services;

public interface IUserService
{
    Task ValidateIfEmailExistsAsync(string email, CancellationToken cancellationToken);
    Task CreateInitialUserAsync(string userName, string email, string password, UserRole role, CancellationToken cancellationToken);
}