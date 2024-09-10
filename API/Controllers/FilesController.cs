using Domain.Files.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class FilesController(IMediator _mediator) : ControllerBase
{
    [HttpPost("upload")]
    [AllowAnonymous]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        => await _mediator.Send(new FileUploadCommand(file), cancellationToken);
}