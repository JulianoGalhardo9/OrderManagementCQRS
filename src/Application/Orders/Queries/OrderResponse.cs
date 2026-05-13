namespace OrderManagement.Application.Orders.Queries;

public record OrderResponse(
    Guid Id, 
    string CustomerName, 
    decimal TotalAmount, 
    DateTime CreatedAt, 
    bool IsCanceled
);