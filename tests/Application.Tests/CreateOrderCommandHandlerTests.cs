using Moq;
using MediatR; // Adicione este using
using OrderManagement.Application.Orders.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using Xunit;

namespace OrderManagement.Application.Tests;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _mediatorMock = new Mock<IMediator>();
        
        _handler = new CreateOrderCommandHandler(_repositoryMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Guid_When_Order_Is_Valid()
    {
        // Arrange
        var command = new CreateOrderCommand("Juliano Galhardo", 150.00m);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        
        // Verifica se o pedido foi salvo
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);

        // Verifica se o evento foi publicado
        _mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}