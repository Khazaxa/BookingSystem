using MediatR;

namespace Core.Cqrs;

public interface IQueryHandler<T, TE> : IRequestHandler<T, TE> where T : IQuery<TE> { }