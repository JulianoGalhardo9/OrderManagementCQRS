using MediatR;
using OrderManagement.Application.Orders.Queries;

namespace OrderManagement.Application.Orders.Queries; 

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
{
    private readonly IOrderReadRepository _readRepository;

    public GetOrderByIdQueryHandler(IOrderReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<OrderResponse?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _readRepository.GetByIdAsync(request.Id);
    }
}