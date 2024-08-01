using Core.Cqrs;
using Core.Exceptions;
using Domain.Desks.Dto;
using Domain.Locations.Dto;
using Domain.Locations.Enums;
using Domain.Locations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Locations.Queries;

public record LocationGetDetailsQuery(int Id) : IQuery<LocationDto?>;

internal class LocationGetDetailsQueryHandler(ILocationRepository _locationRepository) : IQueryHandler<LocationGetDetailsQuery, LocationDto?>
{
    public async Task<LocationDto?> Handle(LocationGetDetailsQuery request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.Query()
                           .Include(l => l.Desks)
                           .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken)
                       ?? throw new DomainException("Location not found", (int)LocationErrorCode.NotFound);

        var locationDto = new LocationDto(
            location.Id,
            location.Name,
            location.Desks?.Select(d => new DeskDto(d.Id, d.Code, d.IsBooked, d.BookedAt, d.BookedUntil)).ToList()
        );

        return locationDto;
    }
}