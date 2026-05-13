using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName, request.TotalAmount);
        
        await Task.CompletedTask;

        return order.Id;
    }
}