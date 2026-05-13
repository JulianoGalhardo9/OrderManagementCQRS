using MediatR;

namespace OrderManagement.Application.Orders.Commands;

public record CreateOrderCommand(string CustomerName, decimal TotalAmount) : IRequest<Guid>;