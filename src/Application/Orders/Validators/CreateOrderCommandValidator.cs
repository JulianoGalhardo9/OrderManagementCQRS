using FluentValidation;
using OrderManagement.Application.Orders.Commands;

namespace OrderManagement.Application.Orders.Validators;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(v => v.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MinimumLength(3).WithMessage("Customer name must be at least 3 characters long.");

        RuleFor(v => v.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
    }
}