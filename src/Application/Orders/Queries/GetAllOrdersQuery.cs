using MediatR;

namespace OrderManagement.Application.Orders.Queries;
public record GetAllOrdersQuery() : IRequest<IEnumerable<OrderResponse>>;