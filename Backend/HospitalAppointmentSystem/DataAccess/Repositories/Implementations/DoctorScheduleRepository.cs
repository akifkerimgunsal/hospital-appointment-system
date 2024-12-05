using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class DoctorScheduleRepository : BaseRepository<DoctorSchedule>, IDoctorScheduleRepository
    {
        private readonly AppDbContext _context;

        public DoctorScheduleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DoctorSchedule> GetDoctorScheduleByDateAsync(Guid doctorId, DateTime date)
        {
            return await _context.DoctorSchedules
                                 .Where(s => s.DoctorId == doctorId && s.WorkDay.Date == date.Date)
                                 .FirstOrDefaultAsync();
        }
        public async Task<List<DateTime>> GetAvailableDatesByDoctorIdAsync(Guid doctorId)
        {
            return await _context.DoctorSchedules
                .Where(schedule => schedule.DoctorId == doctorId && schedule.WorkDay > DateTime.Now)
                .Select(schedule => schedule.WorkDay)
                .Distinct()
                .ToListAsync();
        }

        public async Task AddDoctorScheduleAsync(DoctorSchedule schedule)
        {
            await _context.DoctorSchedules.AddAsync(schedule);
        }

        public async Task DeleteDoctorScheduleAsync(Guid scheduleId)
        {
            var schedule = await _context.DoctorSchedules.FindAsync(scheduleId);
            if (schedule != null)
            {
                _context.DoctorSchedules.Remove(schedule);
            }
        }
    }
}
