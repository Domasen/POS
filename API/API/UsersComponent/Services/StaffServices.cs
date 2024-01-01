using API.UsersComponent.Models;
using API.UsersComponent.Repository;

namespace API.UsersComponent.Services;

public class StaffServices : IStaffServices
{
    private readonly IStaffRepository _staffRepository;
    
    public StaffServices(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }
    public async Task<Staff> AddStaff(Staff staff)
    {
        return await _staffRepository.AddStaff(staff);
    }

    public async Task<Staff?> DeleteStaff(Guid staffId)
    {
        return await _staffRepository.DeleteStaff(staffId);
    }

    public async Task<Staff?> GetStaff(Guid staffId)
    {
        return await _staffRepository.GetStaff(staffId);
    }

    public async Task<IEnumerable<Staff>> GetStaffs()
    {
        return await _staffRepository.GetStaffs();
    }

    public async Task<Staff?> UpdateStaff(Staff staff)
    {
        return await _staffRepository.UpdateStaff(staff);
    }
}