using Domain.Users.Enums;

namespace Domain.Users.Dto;

public class UserDto(int id, string name, string email, UserRole? role)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Email { get; } = email;
    public UserRole? Role { get; } = role;
}