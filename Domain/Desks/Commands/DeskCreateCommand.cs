using Core.Cqrs;
using Domain.Desks.Dto;

namespace Domain.Desks.Commands;

public record DeskCreateCommand(DeskParams Input) : ICommand<int>;

internal class DeskCreateCommandHandler : ICommandHandler<DeskCreateCommand, int>
{
    public Task<int> Handle(DeskCreateCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}