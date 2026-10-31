namespace PruebaTecnica.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
