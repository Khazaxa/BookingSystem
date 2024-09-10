using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Configuration;
using Core.Cqrs;
using Core.Exceptions;
using Domain.Files.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Files.Commands;

public record FileUploadCommand(IFormFile File) : ICommand<IActionResult>;

internal class FileUploadCommandHandler(
    IAppConfiguration _configuration) : ICommandHandler<FileUploadCommand, IActionResult>
{
    public async Task<IActionResult> Handle(FileUploadCommand command, CancellationToken cancellationToken)
    {
        var connectionString = _configuration.Azure?.ConnectionString;
        var containerName = _configuration.Azure?.ContainerName;
        
        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        
        var blobClient = containerClient.GetBlobClient(command.File.FileName);
        if(await blobClient.ExistsAsync(cancellationToken))
            throw new DomainException("File already exists", (int)FileErrorCode.FileAlreadyExists);
        
        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = command.File.ContentType
        };
            
        await blobClient.UploadAsync(command.File.OpenReadStream(), blobHttpHeaders, cancellationToken: cancellationToken);
        
        return new OkResult();
    }
}