namespace PruebaTecnica.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    ICollection<Product> Products  { get; set; }
    public Product Product { get; set; }
    
    
    
}