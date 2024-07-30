using System.ComponentModel.DataAnnotations;
using Core.Database;
using Core.Helpers;
using Domain.Users.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Users.Entities;

internal class User : EntityBase
{
    public const int EmailMaxLength = 64;
    public const int NameMaxLength = 64;

    private User() {}

    public User(string userName, string email, string password, UserRole role)
    {
        UserName = userName;
        Email = email;
        PasswordHash = PasswordHelper.HashPassword(password);
        PasswordSalt = PasswordHelper.GenerateSalt();
        Role = role;
    }
    
    [MaxLength(NameMaxLength)]
    public string UserName { get; private set; }
    [MaxLength(EmailMaxLength)]
    public string Email { get; private set; }
    public byte[] PasswordHash { get; private set; } = null!;
    public byte[] PasswordSalt { get; private set; } = null!;
    public UserRole Role { get; private set; }
    

    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(x => x.Email).IsUnique();
    }
}