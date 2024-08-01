using Domain.Desks.Entities;
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
}