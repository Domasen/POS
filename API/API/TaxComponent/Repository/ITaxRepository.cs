using API.TaxComponent.Models;

namespace API.TaxComponent.Repository;

public interface ITaxRepository
{
    Task<Tax> AddTax(Tax tax);
    Task<Tax?> DeleteTax(Guid taxId);
    Task<Tax?> GetTax(Guid taxId);
    Task<IEnumerable<Tax>> GetTaxes();
    Task<Tax?> UpdateTax (Tax tax);
}