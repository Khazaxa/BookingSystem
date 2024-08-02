using Domain.Desks.Commands;
using Domain.Desks.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class DesksController(IMediator _mediator, ILogger<DesksController> logger) : BaseController(logger)
{
    [HttpPost]
    [Route("desk")]
    [Authorize(Roles = "Admin")]
    public Task<int> CreateDesk(DeskParams input, CancellationToken cancellationToken)
        => _mediator.Send(new DeskCreateCommand(input), cancellationToken);
    
    [HttpPut]
    [Route("desk/{id}/status")]
    [Authorize(Roles = "Admin" + "," + "Employee")]
    public Task<Unit> ChangeDeskStatus(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskChangeStatusCommand(id), cancellationToken);
    
    [HttpDelete]
    [Route("desk/{id}")]
    [Authorize(Roles = "Admin")]
    public Task<Unit> DeleteDesk(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskDeleteCommand(id), cancellationToken);
    
    [HttpPut]
    [Route("desk/{id}/book")]
    [Authorize(Roles = "Admin" + "," + "Employee")]
    public Task<Unit> BookDesk(int id, DateTime bookDate, int days, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        return _mediator.Send(new DeskBookCommand(id, bookDate, days, userId), cancellationToken);
    }
    
    [HttpPut]
    [Route("desk/{id}/unbook")]
    [Authorize(Roles = "Admin" + "," + "Employee")]
    public Task<Unit> UnbookDesk(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskUnbookCommand(id), cancellationToken);
}