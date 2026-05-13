using MediatR;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Orders.Commands;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _repository;

    public CancelOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id);

        if (order == null) return false;

        order.Cancel();

        await _repository.UpdateAsync(order);

        return true;
    }
}