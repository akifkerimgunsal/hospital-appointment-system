using Entities.Concrete;
using Entities.Enum;
using System.Numerics;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByIdentityAsync(string identityNumber);
        Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
        Task UpdateUserProfileAsync(User user);
        Task<User> GetUserByIdentityNumberAsync(string identityNumber);
        Task<IEnumerable<User>> GetDoctorsBySpecialtyAsync(DoctorSpecialty specialty);
        Task<DoctorSchedule> GetDoctorScheduleAsync(Guid doctorId, DateTime date);
        Task UpdateDoctorScheduleAsync(DoctorSchedule schedule);
        Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(Guid doctorId);
    }
}
