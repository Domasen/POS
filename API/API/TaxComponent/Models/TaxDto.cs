namespace API.TaxComponent.Models;

public class TaxDto
{
    public string? TaxName { get; set; }
    public string? TaxDescription { get; set; }
    public Decimal Value { get; set; }
    public TaxCategory Category { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidUntil { get; set; }
}