using Core.Cqrs;
using Core.Exceptions;
using Domain.Desks.Enums;
using Domain.Desks.Repositories;
using MediatR;

namespace Domain.Desks.Commands;

public record DeskChangeStatusCommand(int Id) : ICommand<Unit>;

internal class DeskChangeStatusCommandHandler(
    IDeskRepository _deskRepository, 
    BookingSystemDbContext _dbContext) : IRequestHandler<DeskChangeStatusCommand, Unit>
{
    public async Task<Unit> Handle(DeskChangeStatusCommand command, CancellationToken cancellationToken)
    {
        var desk = await _deskRepository.FindByIdAsync(command.Id, cancellationToken)
                   ?? throw new DomainException($"Desk with provided id: {command.Id} not found", 
                       (int)DeskErrorCode.NotFound);
        
        desk.ChangeStatus();
        _dbContext.Desks.Update(desk);
        await _dbContext.SaveChangesAsync(cancellationToken); 
        return Unit.Value;
    }
}