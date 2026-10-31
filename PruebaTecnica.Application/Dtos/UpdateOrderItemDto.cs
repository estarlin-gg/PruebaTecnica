using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Application.Dtos;

public class UpdateOrderItemDto
{
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public int Quantity { get; set; }
}
