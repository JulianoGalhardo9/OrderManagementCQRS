using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Events;
using OrderManagement.Application.Orders.Events;

namespace OrderManagement.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;
    private readonly IMediator _mediator;

    public CreateOrderCommandHandler(IOrderRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName, request.TotalAmount);

        await _repository.AddAsync(order);

        await _mediator.Publish(new OrderCreatedNotification(new OrderCreatedEvent(order)), cancellationToken);

        return order.Id;
    }
}