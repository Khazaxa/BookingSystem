using System.ComponentModel.DataAnnotations;
using Domain.Users.Enums;

namespace Domain.Users.Dto
{
    public class UserParams
    {
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }
}