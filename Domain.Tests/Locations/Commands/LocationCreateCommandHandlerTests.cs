using Core.Exceptions;
using Domain.Locations.Commands;
using Domain.Locations.Dto;
using Domain.Locations.Entities;
using Domain.Locations.Enums;
using Domain.Locations.Repositories;
using Domain.Locations.Services;
using Moq;
using FluentAssertions;

namespace Domain.Tests.Locations.Commands;

[TestFixture]
public class LocationCreateCommandHandlerTests
{
    [Test]
    public async Task WhenLocationNameExists_ThrowsException()
    {
        // Arrange
        var locationServiceMock = new Mock<ILocationService>();
        locationServiceMock.Setup(x 
                => x.ValidateIfLocationNameExistsAsync(It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ThrowsAsync(new DomainException("Location name already exists", (int)LocationErrorCode.NameInUse));

        var locationRepositoryMock = new Mock<ILocationRepository>();

        var handler = new LocationCreateCommandHandler(locationServiceMock.Object, locationRepositoryMock.Object);

        var command = new LocationCreateCommand(new LocationParams("Location Name"));

        // Act
        var action = new Func<Task>(() => handler.Handle(command, CancellationToken.None));

        // Assert
        await action.Should().ThrowAsync<DomainException>().Where(
            ex => ex.ErrorCode == (int)LocationErrorCode.NameInUse);
    }
    
    [Test]
    public async Task CreatesLocation()
    {
        // Arrange
        var locationServiceMock = new Mock<ILocationService>();
        var locationRepositoryMock = new Mock<ILocationRepository>();

        var location = new Location("Location Name");
        locationRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Location>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(location);

        var handler = new LocationCreateCommandHandler(locationServiceMock.Object, locationRepositoryMock.Object);

        var command = new LocationCreateCommand(new LocationParams("Location Name"));

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        locationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Location>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }
}