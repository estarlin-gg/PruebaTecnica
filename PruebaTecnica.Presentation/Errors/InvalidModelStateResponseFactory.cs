using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PruebaTecnica.Presentation.Errors;

public static class InvalidModelStateResponseFactory
{
    public static IActionResult Create(ActionContext context)
    {
        var errors = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .ToDictionary(
                entry => entry.Key,
                entry => entry.Value!.Errors.Select(error => Translate(entry.Key, error.ErrorMessage)).ToArray());

        var problemDetails = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Solicitud inválida",
            Detail = "Revise los datos enviados e intente nuevamente.",
            Instance = context.HttpContext.Request.Path
        };

        return new BadRequestObjectResult(problemDetails);
    }

    private static string Translate(string field, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return "El valor enviado no es válido.";
        }

        if (message.Contains("field is required", StringComparison.OrdinalIgnoreCase))
        {
            return $"El campo '{field}' es obligatorio.";
        }

        if (message.Contains("could not be converted", StringComparison.OrdinalIgnoreCase))
        {
            return $"El valor enviado para '{field}' no tiene un formato válido.";
        }

        if (message.Contains("is not valid", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("is not a valid", StringComparison.OrdinalIgnoreCase))
        {
            return $"El valor enviado para '{field}' no es válido.";
        }

        return message;
    }
}
