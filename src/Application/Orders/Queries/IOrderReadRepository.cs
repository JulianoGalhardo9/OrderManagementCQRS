using OrderManagement.Application.Orders.Queries;

namespace OrderManagement.Domain.Repositories;
public interface IOrderReadRepository
{
    Task<OrderResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderResponse>> GetAllAsync();
}