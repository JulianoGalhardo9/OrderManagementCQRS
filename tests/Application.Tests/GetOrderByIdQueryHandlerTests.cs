using Moq;
using OrderManagement.Application.Orders.Queries;
using Xunit;

namespace OrderManagement.Application.Tests;

public class GetOrderByIdQueryHandlerTests
{
    private readonly Mock<IOrderReadRepository> _readRepositoryMock;
    private readonly GetOrderByIdQueryHandler _handler;

    public GetOrderByIdQueryHandlerTests()
    {
        _readRepositoryMock = new Mock<IOrderReadRepository>();
        _handler = new GetOrderByIdQueryHandler(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_OrderResponse_When_Order_Exists()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var expectedResponse = new OrderResponse(orderId, "Test Customer", 100.00m, DateTime.UtcNow, false);
        
        // Configuramos o Mock para retornar o pedido esperado quando o ID for solicitado
        _readRepositoryMock.Setup(r => r.GetByIdAsync(orderId))
            .ReturnsAsync(expectedResponse);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.CustomerName, result?.CustomerName);
        Assert.Equal(orderId, result?.Id);
    }
}