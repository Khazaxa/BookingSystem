using Core.Exceptions;
using Domain.Locations.Entities;
using Domain.Locations.Enums;
using Domain.Locations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Locations.Services;

internal class LocationService(
    ILocationRepository _locationRepository, 
    BookingSystemDbContext _dbContext) : ILocationService
{
    public async Task ValidateIfLocationNameExistsAsync(string name, CancellationToken cancellationToken)
    {
        _ = await _locationRepository.FindByNameAsync(name, cancellationToken) 
            ?? throw new DomainException("Location with provided name already exists", 
                (int)LocationErrorCode.NameInUse);
    }
    
    public async Task CreateInitialLocationAsync(string name, CancellationToken cancellationToken)
    {
        if (await _dbContext.Locations.AnyAsync(l => l.Name == name, cancellationToken))
            return;

        var location = new Location(name);

        await _locationRepository.AddAsync(location, cancellationToken);
    }
}