using System.ComponentModel.DataAnnotations;
using Core.Database;
using Domain.Users.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Users.Entities;

internal class User : EntityBase
{
    public const int EmailMaxLength = 64;
    public const int NameMaxLength = 64;

    private User() {}

    public User(string name, string email, byte[] passwordHash, byte[] passwordSalt, UserRole role)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Role = role;
    }
    
    [MaxLength(NameMaxLength)]
    public string Name { get; private set; }
    [MaxLength(EmailMaxLength)]
    public string Email { get; private set; }
    public byte[] PasswordHash { get; private set; } = null!;
    public byte[] PasswordSalt { get; private set; } = null!;
    public UserRole Role { get; private set; }

    public void UpdateRole(UserRole role)
    {
        Role = role;
    }

    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(x => x.Email).IsUnique();
    }
}