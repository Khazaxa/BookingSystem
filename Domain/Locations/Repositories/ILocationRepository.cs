using Domain.Locations.Entities;

namespace Domain.Locations.Repositories;

internal interface ILocationRepository
{
    Task<Location?> FindByNameAsync(string name, CancellationToken cancellationToken);
    Task<Location> AddAsync(Location location, CancellationToken cancellationToken);
}