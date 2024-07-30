using Domain.Users.Entities;

namespace Domain.Users.Repositories;

internal interface IUserRepository
{
   User Add(User user, CancellationToken cancellationToken);
   User? FindByEmail(string email);
}