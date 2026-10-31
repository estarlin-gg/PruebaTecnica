namespace PruebaTecnica.Application.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public List<OrderItemDto> Items { get; set; } = new();

    public decimal Total { get; set; }
}
