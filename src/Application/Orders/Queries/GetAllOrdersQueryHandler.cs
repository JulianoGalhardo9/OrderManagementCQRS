using MediatR;

namespace OrderManagement.Application.Orders.Queries;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderResponse>>
{
    public async Task<IEnumerable<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = new List<OrderResponse>
        {
            new OrderResponse(Guid.NewGuid(), "João Silva", 100.50m, DateTime.UtcNow, false),
            new OrderResponse(Guid.NewGuid(), "Maria Oliveira", 250.00m, DateTime.UtcNow, false)
        };
        return await Task.FromResult(orders);
    }
}