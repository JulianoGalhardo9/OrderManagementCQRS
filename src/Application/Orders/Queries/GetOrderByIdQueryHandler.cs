using MediatR;

namespace OrderManagement.Application.Orders.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
{
    public async Task<OrderResponse?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return null;

        var fakeOrder = new OrderResponse(
            request.Id, 
            "Cliente de Teste", 
            150.00m, 
            DateTime.UtcNow, 
            false
        );

        return await Task.FromResult(fakeOrder);
    }
}