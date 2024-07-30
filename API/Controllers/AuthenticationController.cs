using Domain.Authentication.Commands;
using Domain.Authentication.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("auth")]
public class AuthenticationController(IMediator _mediator) : ControllerBase
{
    [HttpPost, Route("login")]
    public Task<LoginResponseDto> Login(LoginParams loginParams, CancellationToken cancellationToken)
        => _mediator.Send(new LoginCommand(loginParams), cancellationToken);
     
}