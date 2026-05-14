using MediatR;
using OrderManagement.Domain.Events;

namespace OrderManagement.Application.Orders.Events;

public record OrderCreatedNotification(OrderCreatedEvent DomainEvent) : INotification;