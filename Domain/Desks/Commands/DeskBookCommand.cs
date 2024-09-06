using Core.Configuration;
using Core.Cqrs;
using Core.Exceptions;
using Domain.Desks.Entities;
using Domain.Desks.Enums;
using Domain.Desks.Repositories;
using Domain.Users.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Desks.Commands;

public record DeskBookCommand(int Id, DateTime BookDate, int Days, int UserId) : ICommand<Unit>;

internal class DeskBookCommandHandler(
    IDeskRepository _deskRepository, 
    BookingSystemDbContext _dbContext, 
    IAppConfiguration _configuration
) : ICommandHandler<DeskBookCommand, Unit>
{
    public async Task<Unit> Handle(DeskBookCommand command, CancellationToken cancellationToken)
    {
        DateTime bookDate = command.BookDate;
        if (_configuration.Reservations != null)
        {
            var maxDays = _configuration.Reservations.MaxReservationDays;
            var blockChangeTime = _configuration.Reservations.BlockChangeHours;
        
            var existingBooking = await _dbContext.Desks
                .Where(d => d.UserId == command.UserId && d.IsBooked && d.BookedUntil > DateTime.UtcNow)
                .FirstOrDefaultAsync(cancellationToken);
        
            var desk = await _deskRepository.FindByIdAsync(command.Id, cancellationToken)
                       ?? throw new DomainException("Desk not found", (int)DeskErrorCode.NotFound);
        
            ValidateBook(desk, command, maxDays, blockChangeTime, existingBooking);
        
            desk.Book(bookDate, bookDate.AddDays(command.Days), command.UserId);
        
            _dbContext.Desks.Update(desk);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
    
    private void ValidateBook(Desk desk, DeskBookCommand command, int maxDays, int blockChangeTime, Desk? existingBooking)
    {
        if (!desk.IsAvailable)
            throw new DomainException("Desk is not available", (int)DeskErrorCode.DeskIsNotAvailable);
        if (desk.IsBooked)
            throw new DomainException("Desk is already booked", (int)DeskErrorCode.DeskIsBooked);
        if (command.Days > maxDays)
            throw new DomainException($"You can book a desk for a maximum of {maxDays} days", 
                (int)DeskErrorCode.ReservationDaysLimitExceeded);
        if (existingBooking != null 
            && existingBooking.BookedUntil >= DateTime.Now 
            && existingBooking.BookedAt <= DateTime.UtcNow.AddHours(blockChangeTime))
            throw new DomainException("You already have a desk booked", (int)UserErrorCode.DeskAlreadyBooked);
    }
}