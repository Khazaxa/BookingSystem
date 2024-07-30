using Domain.Users.Enums;

namespace Domain.Users.Dto;

public class UserDto(
    string Name, 
    string Email, 
    byte[] PasswordHash,
    byte[] PasswordSalt,
    UserRole? Role
);