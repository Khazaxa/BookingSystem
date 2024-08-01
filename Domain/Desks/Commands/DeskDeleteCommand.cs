using Core.Cqrs;
using Core.Exceptions;
using Domain.Desks.Enums;
using Domain.Desks.Repositories;
using MediatR;

namespace Domain.Desks.Commands;

public record DeskDeleteCommand(int Id) : ICommand<Unit>;

internal class DeskDeleteCommandHandler(IDeskRepository _deskRepository) : IRequestHandler<DeskDeleteCommand, Unit>
{
    public async Task<Unit> Handle(DeskDeleteCommand command, CancellationToken cancellationToken)
    {
        var desk = await _deskRepository.FindByIdAsync(command.Id, cancellationToken) 
                   ?? throw new DomainException("Desk not found", (int)DeskErrorCode.NotFound);
        
        if (desk.IsBooked)
            throw new DomainException("Desk is booked", (int)DeskErrorCode.DeskIsBooked);
       
        await _deskRepository.DeleteDeskAsync(command.Id, cancellationToken);
        return Unit.Value;
    }
}