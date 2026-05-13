namespace OrderManagement.Application.Orders.Queries;

public interface IOrderReadRepository
{
    Task<OrderResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderResponse>> GetAllAsync();
}