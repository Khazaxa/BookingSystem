using Domain.Users.Enums;

namespace Domain.Users.Dto;

public class UserDto
{
    public UserDto(int id, string name, string email, UserRole? role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }
    
    
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public UserRole? Role { get; }
}