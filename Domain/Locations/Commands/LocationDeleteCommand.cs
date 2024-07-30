using Core.Cqrs;
using Core.Exceptions;
using Domain.Locations.Enums;
using Domain.Locations.Repositories;
using MediatR;

namespace Domain.Locations.Commands;

public record LocationDeleteCommand(int Id) : ICommand<Unit>;

internal class LocationDeleteCommandHandler(ILocationRepository _locationRepository) : IRequestHandler<LocationDeleteCommand, Unit>
{
    public async Task<Unit> Handle(LocationDeleteCommand command, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.FindByIdAsync(command.Id, cancellationToken);
        if (location is null)
            throw new DomainException("Location not found", (int)LocationErrorCode.NotFound);
        
        await _locationRepository.DeleteLocationAsync(command.Id, cancellationToken);
        return Unit.Value;
    }
}