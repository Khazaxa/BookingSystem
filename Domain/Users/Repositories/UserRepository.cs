using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Users.Repositories;

internal class UserRepository(BookingSystemDbContext _dbContext) : IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
        => await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    public async Task<IEnumerable<User>> FindAsync(CancellationToken cancellationToken)
        => await _dbContext.Users.ToListAsync(cancellationToken);

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }
}