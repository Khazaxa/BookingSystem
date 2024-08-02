using Core.Cqrs;
using Domain.Locations.Dto;
using Domain.Locations.Entities;
using Domain.Locations.Repositories;
using Domain.Locations.Services;

namespace Domain.Locations.Commands;

public record LocationCreateCommand(LocationParams Input) : ICommand<int>;

internal class LocationCreateCommandHandler(
    ILocationService _locationService, 
    ILocationRepository _locationRepository) : ICommandHandler<LocationCreateCommand, int>
{
    public async Task<int> Handle(LocationCreateCommand command, CancellationToken cancellationToken)
    {
        var input = command.Input;
        await _locationService.ValidateIfLocationNameExistsAsync(input.Name, cancellationToken);
        
        var location = new Location(input.Name);

        var addedLocation = await _locationRepository.AddAsync(location, cancellationToken);

        return addedLocation.Id;
    }
}