using Domain.Files.Commands;
using Domain.Files.Queries;
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
    
    [HttpGet("download")]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadFile(string fileName, CancellationToken cancellationToken)
        => await _mediator.Send(new FileDownloadQuery(fileName), cancellationToken);
}