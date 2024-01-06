using API.Data;
using API.TaxComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.TaxComponent.Repository;

public class TaxRepository : ITaxRepository
{
    private readonly DataContext _context;

    public TaxRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Tax> AddTax(Tax tax)
    {
        tax.Id = Guid.NewGuid();
        var result = await _context.Taxes.AddAsync(tax);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Tax?> DeleteTax(Guid taxId)
    {
        var result = await _context.Taxes.FirstOrDefaultAsync(t => t.Id == taxId);

        if (result != null)
        {
            _context.Taxes.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<Tax?> GetTax(Guid taxId)
    {
        return await _context.Taxes.FirstOrDefaultAsync(t => t.Id == taxId);

    }

    public async Task<IEnumerable<Tax>> GetTaxes()
    {
        return await _context.Taxes.ToListAsync();
    }

    public async Task<Tax?> UpdateTax(Tax tax)
    {
        var result = await _context.Taxes.FirstOrDefaultAsync(t => t.Id == tax.Id);

        if (result != null)
        {
            result.Id = tax.Id;
            result.Category = tax.Category;
            result.Value = tax.Value;
            result.TaxDescription = tax.TaxDescription;
            result.TaxName = tax.TaxName;
            result.ValidFrom = tax.ValidFrom;
            result.ValidUntil = tax.ValidUntil;
            
            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}