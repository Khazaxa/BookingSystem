using Domain.Locations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Locations.Repositories;

internal class LocationRepository(BookingSystemDbContext _dbContext) : ILocationRepository
{
    public async Task<Location?> FindByNameAsync(string name, CancellationToken cancellationToken)
        => await _dbContext.Locations.FirstOrDefaultAsync(l => l.Name == name, cancellationToken);

    public async Task<Location> AddAsync(Location location, CancellationToken cancellationToken)
    {
        await _dbContext.Locations.AddAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return location;
    }
}