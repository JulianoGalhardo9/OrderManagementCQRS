using MediatR;

namespace OrderManagement.Application.Orders.Commands;
public record CancelOrderCommand(Guid Id) : IRequest<bool>;