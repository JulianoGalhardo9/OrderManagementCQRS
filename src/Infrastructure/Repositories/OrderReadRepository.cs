using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagement.Application.Orders.Queries;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Infrastructure.Repositories;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly string _connectionString;

    public OrderReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        
        const string sql = "SELECT Id, CustomerName, TotalAmount, CreatedAt, IsCanceled FROM Orders WHERE Id = @Id";
        
        return await connection.QueryFirstOrDefaultAsync<OrderResponse>(sql, new { Id = id });
    }

    public async Task<IEnumerable<OrderResponse>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        
        const string sql = "SELECT Id, CustomerName, TotalAmount, CreatedAt, IsCanceled FROM Orders";
        
        return await connection.QueryAsync<OrderResponse>(sql);
    }
}