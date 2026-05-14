using MediatR;

namespace OrderManagement.Application.Orders.Events;

public class SendEmailOrderCreatedHandler : INotificationHandler<OrderCreatedNotification>
{
    public Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        var order = notification.DomainEvent.Order;
        Console.WriteLine($"[SIMULAÇÃO] E-mail enviado para o cliente {order.CustomerName} sobre o pedido {order.Id}");
        return Task.CompletedTask;
    }
}