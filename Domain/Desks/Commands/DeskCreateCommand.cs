using Core.Cqrs;
using Core.Exceptions;
using Domain.Desks.Dto;
using Domain.Desks.Entities;
using Domain.Desks.Enums;
using Domain.Desks.Repositories;
using Domain.Locations.Enums;
using Domain.Locations.Repositories;

namespace Domain.Desks.Commands;

public record DeskCreateCommand(DeskParams Input) : ICommand<int>;

internal class DeskCreateCommandHandler(IDeskRepository _deskRepository,
    ILocationRepository _locationRepository): ICommandHandler<DeskCreateCommand, int>
{
    public async Task<int> Handle(DeskCreateCommand command, CancellationToken cancellationToken)
    {
        var lastDesk = await _deskRepository.GetLastDeskAsync(cancellationToken);
        var newCode = lastDesk == null ? "001" : (int.Parse(lastDesk.Code) + 1).ToString("D3");
        
        Validate(command.Input, newCode, cancellationToken);

        var desk = new Desk(
            newCode,
            command.Input.LocationId
        );
        
        var addedDesk = await _deskRepository.AddAsync(desk, cancellationToken);
        return addedDesk.Id;
    }
    
    private async void Validate(DeskParams input, string code, CancellationToken cancellationToken)
    {
        if(await _locationRepository.FindByIdAsync(input.LocationId, cancellationToken) == null)
            throw new DomainException($"Location with provided id: {input.LocationId} was not found",
                (int)LocationErrorCode.NotFound);
        if(code.Length > 3)
            throw new DomainException("Max number of desks achieved", (int)DeskErrorCode.InvalidCode);
    }
}