using Domain.Desks.Commands;
using Domain.Desks.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class DesksController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    [Route("desk")]
    public Task<int> CreateDesk(DeskParams input, CancellationToken cancellationToken)
        => _mediator.Send(new DeskCreateCommand(input), cancellationToken);
    
    [HttpPut]
    [Route("desk/{id}")]
    public Task<Unit> ChangeDeskStatus(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskChangeStatusCommand(id), cancellationToken);
    
    [HttpDelete]
    [Route("desk/{id}")]
    public Task<Unit> DeleteDesk(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskDeleteCommand(id), cancellationToken);
}