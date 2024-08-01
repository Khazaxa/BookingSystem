using Domain.Locations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Locations.Repositories;

internal class LocationRepository(BookingSystemDbContext _dbContext) : ILocationRepository
{
    public Task<Location?> FindById(int id)
        => Task.FromResult(_dbContext.Locations.FirstOrDefault(l => l.Id == id));
    
    public async Task<Location?> FindByIdAsync(int id, CancellationToken cancellationToken)
        => await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    
    public async Task<Location?> FindByNameAsync(string name, CancellationToken cancellationToken)
        => await _dbContext.Locations.FirstOrDefaultAsync(l => l.Name == name, cancellationToken);

    public async Task<Location> AddAsync(Location location, CancellationToken cancellationToken)
    {
        await _dbContext.Locations.AddAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return location;
    }

    public async Task DeleteLocationAsync(int id, CancellationToken cancellationToken)
    {
        var location = await FindByIdAsync(id, cancellationToken);
        if (location is null)
            return;
       
        _dbContext.Locations.Remove(location);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsLocationContainsDeskAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Locations
            .AnyAsync(l => l.Id == id && l.Desks.Any(), cancellationToken);
    }
    
    public IQueryable<Location> Query()
        => _dbContext.Locations.AsQueryable();
}