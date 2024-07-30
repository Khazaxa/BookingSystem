namespace Domain.Users.Services;

public interface IUserService
{
    Task ValidateIfEmailExistsAsync(string email, CancellationToken cancellationToken);
    Task CreateInitialUserAsync(string email, string password, CancellationToken cancellationToken);
}