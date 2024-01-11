using API.TaxComponent.Models;

namespace API.TaxComponent.Services;

public interface ITaxServices
{
    Task<Tax> AddTax(TaxDto tax);
    Task<Tax?> DeleteTax(Guid taxId);
    Task<Tax?> GetTax(Guid taxId);
    Task<IEnumerable<Tax>> GetTaxes();
    Task<Tax?> UpdateTax (Tax tax);
}