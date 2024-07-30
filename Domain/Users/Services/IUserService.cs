namespace Domain.Users.Services;

public interface IUserService
{
    void ValidateIfEmailExists(string email);
}