using Domain.Users.Commands;
using Domain.Users.Dto;
using Domain.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("users")]
public class UsersController(IMediator _mediator) : ControllerBase
{
    [HttpPost, Route("user")]
    [AllowAnonymous]
    public Task<int> CreateUser(UserParams input, CancellationToken cancellationToken)
        => _mediator.Send(new UserCreateCommand(input), cancellationToken);
    
    [HttpGet, Route("")]
    [AllowAnonymous]
    public Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
        => _mediator.Send(new UsersQuery(), cancellationToken);
}