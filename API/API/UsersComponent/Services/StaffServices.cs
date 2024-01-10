using API.ServicesComponent.Models;
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

    private static void AddAvailableTimeSlots(List<Staff> employees, List<DateModel> timeSlots)
    {
        foreach (var employee in employees)
        {
            foreach (var timeSlot in timeSlots)
            {
                // Check if the employee is available during this time slot
                if (IsEmployeeAvailable(employee, timeSlot))
                {
                    // Create DateTime objects with the corresponding date and time
                    DateTime date = timeSlot.ActualDates[0].ActualDate.Date;
                    DateTime startTime = date.Add(timeSlot.ActualDates[0].StartTime.TimeOfDay);
                    DateTime endTime = date.Add(timeSlot.ActualDates[0].EndTime.TimeOfDay);

                    // Add this time slot to the employee's available time slots
                    employee.AvailableTimeSlots.Add(new TimeSlot
                    {
                        StartTime = startTime,
                        EndTime = endTime
                    });
                }
            }
        }
    }

    private static bool IsEmployeeAvailable(Staff staff, DateModel timeSlot)
    {
        foreach (var dateItem in timeSlot.ActualDates)
        {
            DateTime date = dateItem.ActualDate.Date;
            DateTime startTime = date.Add(dateItem.StartTime.TimeOfDay);
            DateTime endTime = date.Add(dateItem.EndTime.TimeOfDay);

            // Check for overlaps with existing time slots
            if (staff.AvailableTimeSlots.Any(existingSlot =>
                    !(endTime <= existingSlot.StartTime || startTime >= existingSlot.EndTime)))
            {
                return false; // Employee is busy during this time slot
            }
        }

        return true; // Employee is available during this time slot
    }


}