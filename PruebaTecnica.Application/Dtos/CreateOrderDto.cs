namespace PruebaTecnica.Application.Dtos;

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; } =  DateTime.Now;
    
    public List<ProductDto> Products { get; set; }
    
}