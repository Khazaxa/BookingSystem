using Core.Cqrs;
using Core.Exceptions;
using Domain.Desks.Enums;
using Domain.Desks.Repositories;
using MediatR;

namespace Domain.Desks.Commands;

public record DeskUnbookCommand(int Id) : ICommand<Unit>;

internal class DeskUnbookCommandHandler(
    IDeskRepository _deskRepository, 
    BookingSystemDbContext _dbContext) : ICommandHandler<DeskUnbookCommand, Unit>
{
    public async Task<Unit> Handle(DeskUnbookCommand command, CancellationToken cancellationToken)
    {
        var desk = await _deskRepository.FindByIdAsync(command.Id, cancellationToken)
                   ?? throw new DomainException("Desk not found", (int)DeskErrorCode.NotFound);
        
        if (!desk.IsBooked)
            throw new DomainException("Desk is not booked", (int)DeskErrorCode.DeskIsNotBooked);
        
        desk.Unbook();
        
        _dbContext.Desks.Update(desk);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}