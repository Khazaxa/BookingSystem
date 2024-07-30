using Domain.Users.Enums;

namespace Domain.Users.Dto
{
    public class UserParams
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}