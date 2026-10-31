using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Application.Dtos;

public class CreateOrderItemDto
{
    [Required(ErrorMessage = "El producto es obligatorio.")]
    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public int Quantity { get; set; }
}
