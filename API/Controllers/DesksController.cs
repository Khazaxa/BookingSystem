using Domain.Desks.Commands;
using Domain.Desks.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class DesksController(IMediator _mediator, ILogger<DesksController> logger) : BaseController(logger)
{
    [HttpPost]
    [Route("desk")]
    public Task<int> CreateDesk(DeskParams input, CancellationToken cancellationToken)
        => _mediator.Send(new DeskCreateCommand(input), cancellationToken);
    
    [HttpPut]
    [Route("desk/{id}/status")]
    public Task<Unit> ChangeDeskStatus(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskChangeStatusCommand(id), cancellationToken);
    
    [HttpDelete]
    [Route("desk/{id}")]
    public Task<Unit> DeleteDesk(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskDeleteCommand(id), cancellationToken);
    
    [HttpPut]
    [Route("desk/{id}/book")]
    public Task<Unit> BookDesk(int id, DateTime bookDate, int days, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        return _mediator.Send(new DeskBookCommand(id, bookDate, days, userId), cancellationToken);
    }
    
    [HttpPut]
    [Route("desk/{id}/unbook")]
    public Task<Unit> UnbookDesk(int id, CancellationToken cancellationToken)
        => _mediator.Send(new DeskUnbookCommand(id), cancellationToken);
}