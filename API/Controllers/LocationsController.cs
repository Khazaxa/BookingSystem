using Domain.Locations.Commands;
using Domain.Locations.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class LocationsController(IMediator _mediator) : ControllerBase
{
    [HttpPost, Route("location")]
    [AllowAnonymous]
    public async Task<int> CreateLocation(LocationParams input, CancellationToken cancellationToken)
        => await _mediator.Send(new LocationCreateCommand(input), cancellationToken);
    
    [HttpDelete]
    [Route("location/{id}")]
    [AllowAnonymous]
    public async Task<Unit> DeleteLocation(int id, CancellationToken cancellationToken)
        => await _mediator.Send(new LocationDeleteCommand(id), cancellationToken);
    

}