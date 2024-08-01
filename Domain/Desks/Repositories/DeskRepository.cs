using Core.Exceptions;
using Domain.Desks.Entities;
using Domain.Desks.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Desks.Repositories;

internal class DeskRepository(BookingSystemDbContext _dbContext) : IDeskRepository
{
    public async Task<Desk> AddAsync(Desk desk, CancellationToken cancellationToken)
    {
        await _dbContext.Desks.AddAsync(desk, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return desk;
    }
    
    public async Task<Desk?> GetLastDeskAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Desk>()
            .OrderByDescending(d => d.Code)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Desk?> FindByIdAsync(int id, CancellationToken cancellationToken)
        => await _dbContext.Desks.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

    public async Task DeleteDeskAsync(int id, CancellationToken cancellationToken)
    {
        var desk = await FindByIdAsync(id, cancellationToken) 
                   ?? throw new DomainException("Desk not found", (int)DeskErrorCode.NotFound);
    
        _dbContext.Desks.Remove(desk);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}