using API.Data;
using API.UsersComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.UsersComponent.Repository;

public class StaffRepository : IStaffRepository
{
    private readonly DataContext _context;
    public StaffRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<Staff> AddStaff(Staff staff)
    {
        var result = await _context.Staffs.AddAsync(staff);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Staff?> DeleteStaff(Guid staffId)
    {
        var result = await _context.Staffs.FirstOrDefaultAsync(s => s.Id == staffId);

        if (result != null)
        {
            _context.Staffs.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<Staff?> GetStaff(Guid staffId)
    {
        return await _context.Staffs.FirstOrDefaultAsync(s => s.Id == staffId);
    }

    public async Task<IEnumerable<Staff>> GetStaffs()
    {
        return await _context.Staffs.ToListAsync();
    }

    public async Task<Staff?> UpdateStaff(Staff staff)
    {
        var result = await _context.Staffs.FirstOrDefaultAsync(s => s.Id == staff.Id);

        if (result != null)
        {
            result.Id = staff.Id;
            result.FirstName = staff.FirstName;
            result.LastName = staff.LastName;
            result.PhoneNumber = staff.PhoneNumber;
            result.Email = staff.Email;
            result.HireDate = staff.HireDate;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}