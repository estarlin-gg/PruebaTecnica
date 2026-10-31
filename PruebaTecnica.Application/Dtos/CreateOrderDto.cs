using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Application.Dtos;

public class CreateOrderDto
{
    [Required(ErrorMessage = "El cliente es obligatorio.")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "Los productos de la orden son obligatorios.")]
    [MinLength(2, ErrorMessage = "La orden debe incluir al menos dos productos.")]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}
