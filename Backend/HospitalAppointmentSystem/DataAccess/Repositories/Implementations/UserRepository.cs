using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities.Concrete;
using Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DataAccess.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdentityAsync(string identityNumber)
        {
            return await _context.Users
                                 .Where(u => u.IdentityNumber == identityNumber)
                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _context.Users
                                 .Where(u => u.UserRole == role)
                                 .ToListAsync();
        }
        public async Task<User> GetUserByIdentityNumberAsync(string identityNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.IdentityNumber == identityNumber);
        }


        public async Task UpdateUserProfileAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;

                _context.Users.Update(existingUser);
            }
        }

        public async Task<IEnumerable<User>> GetDoctorsBySpecialtyAsync(DoctorSpecialty specialty)
        {
            return await _context.Users
                                 .Where(d => d.DoctorSpecialty == specialty)
                                 .ToListAsync();
        }

        public async Task<DoctorSchedule> GetDoctorScheduleAsync(Guid doctorId, DateTime date)
        {
            return await _context.DoctorSchedules
                                 .Where(s => s.DoctorId == doctorId && s.WorkDay.Date == date.Date)
                                 .FirstOrDefaultAsync();
        }

        public async Task UpdateDoctorScheduleAsync(DoctorSchedule schedule)
        {
            var existingSchedule = await _context.DoctorSchedules.FindAsync(schedule.Id);
            if (existingSchedule != null)
            {
                existingSchedule.StartTime = schedule.StartTime;
                existingSchedule.EndTime = schedule.EndTime;
                _context.DoctorSchedules.Update(existingSchedule);
            }
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(Guid doctorId)
        {
            return await _context.Appointments
                                 .Where(a => a.DoctorId == doctorId && a.AppointmentDate >= DateTime.UtcNow)
                                 .ToListAsync();
        }
    }
}
