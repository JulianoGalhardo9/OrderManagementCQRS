using MediatR;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Orders.Commands;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _repository;

    public UpdateOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id);

        if (order == null) return false;
        
        return true;
    }
}