using MediatR;

namespace OrderManagement.Application.Orders.Queries;
public record GetOrderByIdQuery(Guid Id) : IRequest<OrderResponse?>;