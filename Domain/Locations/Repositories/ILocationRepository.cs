using Domain.Locations.Entities;

namespace Domain.Locations.Repositories;

internal interface ILocationRepository
{
    Task<Location?> FindByIdAsync(int id, CancellationToken cancellationToken);
    Task<Location?> FindByNameAsync(string name, CancellationToken cancellationToken);
    Task<Location> AddAsync(Location location, CancellationToken cancellationToken);
    Task DeleteLocationAsync(int id, CancellationToken cancellationToken);
    Task<bool> IsLocationContainsDeskAsync(int id, CancellationToken cancellationToken);
}