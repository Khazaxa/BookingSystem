using Core.Configuration;
using Core.Cqrs;
using Core.Exceptions;
using Domain.Desks.Enums;
using Domain.Desks.Repositories;
using MediatR;

namespace Domain.Desks.Commands;

public record DeskBookCommand(int Id, int Days) : ICommand<Unit>;

internal class DeskBookCommandHandler(
    IDeskRepository _deskRepository, 
    BookingSystemDbContext _dbContext, 
    IAppConfiguration _configuration
    ) : ICommandHandler<DeskBookCommand, Unit>
{
    public async Task<Unit> Handle(DeskBookCommand command, CancellationToken cancellationToken)
    {
        var maxDays = _configuration.Reservations.MaxReservationDays;
        
        var desk = await _deskRepository.FindByIdAsync(command.Id, cancellationToken)
            ?? throw new DomainException("Desk not found", (int)DeskErrorCode.NotFound);

        if (!desk.IsAvailable)
            throw new DomainException("Desk is not available", (int)DeskErrorCode.DeskIsNotAvailable);
        if (desk.IsBooked)
            throw new DomainException("Desk is already booked", (int)DeskErrorCode.DeskIsBooked);
        if (command.Days > maxDays)
            throw new DomainException($"You can book a desk for a maximum of {maxDays} days", 
                (int)DeskErrorCode.ReservationDaysLimitExceeded);
        
        desk.Book(DateTime.UtcNow, DateTime.UtcNow.AddDays(command.Days));
        
        _dbContext.Desks.Update(desk);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}