using API.TaxComponent.Models;
using API.TaxComponent.Repository;

namespace API.TaxComponent.Services;

public class TaxServices : ITaxServices
{
    private readonly ITaxRepository _taxRepository;
    
    public TaxServices(ITaxRepository taxRepository)
    {
        _taxRepository = taxRepository;
    }
    
    public async Task<Tax> AddTax(Tax tax)
    {
        return await _taxRepository.AddTax(tax);
    }

    public async Task<Tax?> DeleteTax(Guid taxId)
    {
        return await _taxRepository.DeleteTax(taxId);
    }

    public async Task<Tax?> GetTax(Guid taxId)
    {
        return await _taxRepository.GetTax(taxId);
    }

    public async Task<IEnumerable<Tax>> GetTaxes()
    {
        return await _taxRepository.GetTaxes();
    }

    public async Task<Tax?> UpdateTax(Tax tax)
    {
        return await _taxRepository.UpdateTax(tax);
    }
}