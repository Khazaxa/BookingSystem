using Core.Exceptions;
using Domain.Locations.Commands;
using Domain.Locations.Entities;
using Domain.Locations.Enums;
using Domain.Locations.Repositories;
using FluentAssertions;
using Moq;

namespace Domain.Tests.Locations.Commands;

[TestFixture]
public class LocationDeleteCommandHandlerTests
{
    [Test]
    public async Task WhenLocationExists_DeletesLocation()
    {
        // Arrange
        var locationRepositoryMock = new Mock<ILocationRepository>();
        locationRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<int>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Location("Location Name"));

        var handler = new LocationDeleteCommandHandler(locationRepositoryMock.Object);
        var command = new LocationDeleteCommand(1);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        locationRepositoryMock.Verify(x => x.DeleteLocationAsync(It.IsAny<int>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Test]
    public async Task WhenLocationContainsDesks_ThrowsException()
    {
        // Arrange
        var locationRepositoryMock = new Mock<ILocationRepository>();
        locationRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<int>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Location("Location Name"));
        locationRepositoryMock.Setup(x => x.IsLocationContainsDeskAsync(It.IsAny<int>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new LocationDeleteCommandHandler(locationRepositoryMock.Object);
        var command = new LocationDeleteCommand(1);

        // Act
        var action = new Func<Task>(() => handler.Handle(command, CancellationToken.None));

        // Assert
        await action.Should().ThrowAsync<DomainException>()
            .Where(ex => ex.ErrorCode == (int)LocationErrorCode.ContainsDesks);
    }
}