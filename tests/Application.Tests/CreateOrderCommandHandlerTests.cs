using Moq;
using OrderManagement.Application.Orders.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using Xunit;

namespace OrderManagement.Application.Tests;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _handler = new CreateOrderCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Guid_When_Order_Is_Valid()
    {
        // Arrange (Organizar)
        var command = new CreateOrderCommand("Juliano Galhardo", 150.00m);

        // Act (Agir)
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert (Verificar)
        Assert.NotEqual(Guid.Empty, result);
        
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
    }
}