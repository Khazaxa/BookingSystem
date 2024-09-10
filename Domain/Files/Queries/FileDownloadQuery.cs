using Azure.Storage.Blobs;
using Core.Configuration;
using Core.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Files.Queries;

public record FileDownloadQuery(string FileName) : IQuery<IActionResult>;

internal class FileDownloadQueryHandler(IAppConfiguration _configuration) : IQueryHandler<FileDownloadQuery, IActionResult>
{
    public async Task<IActionResult> Handle(FileDownloadQuery query, CancellationToken cancellationToken)
    {
        var connectionString = _configuration.Azure?.ConnectionString;
        var containerName = _configuration.Azure?.ContainerName;
        
        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        
        var blobClient = containerClient.GetBlobClient(query.FileName);
        if(!await blobClient.ExistsAsync(cancellationToken))
            return new NotFoundResult();

        var downloadResponse = await blobClient.DownloadContentAsync(cancellationToken);
        var content = downloadResponse.Value.Content.ToStream();
        var contentType = (await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken))
            .Value.ContentType;
        
        return new FileStreamResult(content, contentType)
        {
            FileDownloadName = query.FileName
        };
    }
}