using API.UsersComponent.Models;

namespace API.UsersComponent.Repository;

public interface IStaffRepository
{
    Task<Staff> AddStaff();
    Task<Staff> DeleteStaff();
    Task<Staff> GetStaff();
    Task<IEnumerable<Staff>> GetStaffs();
    Task<Staff> UpdateStaff();
}