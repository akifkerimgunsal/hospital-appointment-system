using Entities.Concrete;

namespace DataAccess.Repositories.Interfaces
{
    public interface IDoctorScheduleRepository : IBaseRepository<DoctorSchedule>
    {
        Task<DoctorSchedule> GetDoctorScheduleByDateAsync(Guid doctorId, DateTime date);
        Task AddDoctorScheduleAsync(DoctorSchedule schedule);
        Task<List<DateTime>> GetAvailableDatesByDoctorIdAsync(Guid doctorId);
        Task DeleteDoctorScheduleAsync(Guid scheduleId);
    }
}
