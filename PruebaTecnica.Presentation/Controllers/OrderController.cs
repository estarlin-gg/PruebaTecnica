using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Application.Dtos;
using PruebaTecnica.Application.Interfaces;

namespace PruebaTecnica.Presentation.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IInvoiceService _invoiceService;

    public OrderController(IOrderService orderService, IInvoiceService invoiceService)
    {
        _orderService = orderService;
        _invoiceService = invoiceService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<OrderDto>> CreateAsync([FromBody] CreateOrderDto orderDto, CancellationToken cancellationToken)
    {
        var order = await _orderService.CreateOrderAsync(orderDto, cancellationToken);

        return CreatedAtRoute("GetOrderById", new { id = order.Id }, order);
    }

    [HttpGet("{id:guid}", Name = "GetOrderById")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderAsync(id, cancellationToken);

        return Ok(order);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<OrderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetOrdersAsync(cancellationToken);

        return Ok(orders);
    }

    [HttpPatch("{orderId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<OrderDto>> UpdateItemQuantityAsync(
        Guid orderId,
        Guid itemId,
        [FromBody] UpdateOrderItemDto orderItemDto,
        CancellationToken cancellationToken)
    {
        var order = await _orderService.UpdateOrderItemQuantityAsync(orderId, itemId, orderItemDto, cancellationToken);

        return Ok(order);
    }

    [HttpDelete("{orderId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveItemAsync(Guid orderId, Guid itemId, CancellationToken cancellationToken)
    {
        await _orderService.RemoveOrderItemAsync(orderId, itemId, cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:guid}/invoice")]
    [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceDto>> GetInvoiceAsync(Guid id, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceService.GetInvoiceAsync(id, cancellationToken);

        return Ok(invoice);
    }
}
