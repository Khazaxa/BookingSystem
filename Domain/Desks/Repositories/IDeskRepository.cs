using Domain.Desks.Entities;

namespace Domain.Desks.Repositories;

internal interface IDeskRepository
{
    Task<Desk> AddAsync(Desk desk, CancellationToken cancellationToken);
    Task<Desk?> GetLastDeskAsync(CancellationToken cancellationToken);
    Task<Desk?> FindByIdAsync(int id, CancellationToken cancellationToken);
    Task DeleteDeskAsync(int id, CancellationToken cancellationToken);
}