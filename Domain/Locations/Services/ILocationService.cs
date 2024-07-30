namespace Domain.Locations.Services;

public interface ILocationService
{
    Task ValidateIfLocationNameExistsAsync(string name, CancellationToken cancellationToken);
    Task CreateInitialLocationAsync(string name, CancellationToken cancellationToken);
}