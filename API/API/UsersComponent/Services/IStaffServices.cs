using API.UsersComponent.Models;

namespace API.UsersComponent.Services;

public interface IStaffServices
{
    Task<Staff> AddStaff(StaffDto staff);
    Task<Staff?> DeleteStaff(Guid staffId);
    Task<Staff?> GetStaff(Guid staffId);
    Task<IEnumerable<Staff>> GetStaffs();
    Task<Staff?> UpdateStaff(Staff staff);
}