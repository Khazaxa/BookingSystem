using Domain.Locations.Commands;
using Domain.Locations.Dto;
using Domain.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class LocationsController(IMediator _mediator) : ControllerBase
{
    [HttpPost, Route("location")]
    [Authorize(Roles = "Admin")]
    public async Task<int> CreateLocation(LocationParams input, CancellationToken cancellationToken)
        => await _mediator.Send(new LocationCreateCommand(input), cancellationToken);
    
    [HttpDelete]
    [Route("location/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Unit> DeleteLocation(int id, CancellationToken cancellationToken)
        => await _mediator.Send(new LocationDeleteCommand(id), cancellationToken);
    
    [HttpGet]
    [Route("location/{id}")]
    [Authorize(Roles = "Admin" + "," + "Employee")]
    public async Task<LocationDto?> GetLocationDetails(int id, CancellationToken cancellationToken)
        => await _mediator.Send(new LocationGetDetailsQuery(id), cancellationToken);
}