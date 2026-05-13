using FluentValidation;
using OrderManagement.Application.Orders.Commands;

namespace OrderManagement.Application.Orders.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.CustomerName).NotEmpty().MinimumLength(3);
        RuleFor(v => v.TotalAmount).GreaterThan(0);
    }
}