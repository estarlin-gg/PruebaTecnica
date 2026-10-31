namespace PruebaTecnica.Application.Options;

public class InvoiceOptions
{
    public const string SectionName = "Invoice";

    public decimal TaxRate { get; set; } = 0.18m;
}
