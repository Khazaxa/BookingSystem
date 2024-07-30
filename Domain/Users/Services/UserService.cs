using System.Security.Cryptography;
using System.Text;
using Core.Exceptions;
using Domain.Users.Entities;
using Domain.Users.Enums;
using Domain.Users.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Users.Services;

internal class UserService(IUserRepository _userRepository, BookingSystemDbContext _dbContext) : IUserService
{
    public async Task ValidateIfEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByEmailAsync(email, cancellationToken);
        if (user is not null)
            throw new DomainException("User with provided email already exists", (int)UserErrorCode.EmailInUse);
    }
    
    public async Task CreateInitialUserAsync(string userName, string email, string password, UserRole role, CancellationToken cancellationToken)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken))
            return;

        using var hmac = new HMACSHA512();
        var user = new User(
            userName,
            email,
            hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            hmac.Key,
            role
        );

        await _userRepository.AddAsync(user, cancellationToken);
    }
}