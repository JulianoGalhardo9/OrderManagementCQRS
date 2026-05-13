using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Orders.Commands;
using OrderManagement.Application.Orders.Queries; 

namespace OrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _mediator;

    public OrdersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = orderId }, orderId);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetOrderByIdQuery(id));

        return response == null ? NotFound() : Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(response);
    }
}