using Domain.Users.Entities;

namespace Domain.Users.Repositories;

internal interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<User>> FindAsync(CancellationToken cancellationToken);
    Task<User> AddAsync(User user, CancellationToken cancellationToken);
}