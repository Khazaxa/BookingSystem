using Domain.Users.Entities;

namespace Domain.Users.Repositories;

internal class UserRepository(BookingSystemDbContext _dbContext) : IUserRepository
{
    public User Add(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }
    
    public User? FindByEmail(string email) => _dbContext.Users.FirstOrDefault(u => u.Email == email);
    
}