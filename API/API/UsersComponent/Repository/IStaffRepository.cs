using API.UsersComponent.Models;

namespace API.UsersComponent.Repository;

public interface IStaffRepository
{
    Task<Staff> AddStaff(Staff staff);
    Task<Staff?> DeleteStaff(Guid staffId);
    Task<Staff?> GetStaff(Guid staffId);
    Task<IEnumerable<Staff>> GetStaffs();
    Task<Staff?> UpdateStaff(Staff staff);
}