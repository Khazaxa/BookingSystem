using Domain.Users.Enums;

namespace Domain.Authentication.Services;

public interface IAuthenticationService
{
    string GenerateToken(string userName, UserRole role);
    byte[] ComputePasswordHash(string password, byte[] salt);
}