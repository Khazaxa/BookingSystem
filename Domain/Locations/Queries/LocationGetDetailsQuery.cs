using Core.Cqrs;
using Core.Exceptions;
using Domain.Authentication.Services;
using Domain.Desks.Dto;
using Domain.Locations.Dto;
using Domain.Locations.Enums;
using Domain.Locations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Locations.Queries;

public record LocationGetDetailsQuery(int Id) : IQuery<LocationDto?>;

internal class LocationGetDetailsQueryHandler(
    ILocationRepository _locationRepository, 
    IUserContextService _userContextService) : IQueryHandler<LocationGetDetailsQuery, LocationDto?>
{
    public async Task<LocationDto?> Handle(LocationGetDetailsQuery request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.Query()
                           .Include(l => l.Desks)
                           .ThenInclude(d => d.User)
                           .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken)
                       ?? throw new DomainException("Location not found", (int)LocationErrorCode.NotFound);

        var isAdmin = _userContextService.IsAdmin();

        var locationDto = new LocationDto(
            location.Id,
            location.Name,
            location.Desks?.Select(d => new DeskDto(
                d.Id, 
                d.Code, 
                d.IsAvailable,
                d.IsBooked, 
                d.BookedAt, 
                d.BookedUntil, 
                isAdmin ? d.UserId : null
            )).ToList()
        );

        return locationDto;
    }
}