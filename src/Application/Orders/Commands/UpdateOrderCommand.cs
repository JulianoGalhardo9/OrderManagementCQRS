using MediatR;

namespace OrderManagement.Application.Orders.Commands;
public record UpdateOrderCommand(Guid Id, string CustomerName, decimal TotalAmount) : IRequest<bool>;