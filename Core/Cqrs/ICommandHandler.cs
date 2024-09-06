using MediatR;

namespace Core.Cqrs;

public interface ICommandHandler<T, TE> : IRequestHandler<T, TE> where T : ICommand<TE> { }