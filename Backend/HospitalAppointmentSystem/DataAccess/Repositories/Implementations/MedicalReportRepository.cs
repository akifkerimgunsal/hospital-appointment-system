using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class MedicalReportRepository : BaseRepository<MedicalReport>, IMedicalReportRepository
    {
        private readonly AppDbContext _context;

        public MedicalReportRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalReport>> GetMedicalReportsByAppointmentAsync(Guid appointmentId)
        {
            return await _context.MedicalReports
                                 .Where(m => m.AppointmentId == appointmentId)
                                 .ToListAsync();
        }

        public async Task SubmitMedicalReportAsync(MedicalReport report)
        {
            await _context.MedicalReports.AddAsync(report);
        }
        public async Task<List<MedicalReport>> GetMedicalReportsByAppointmentIdAsync(Guid appointmentId)
        {
            return await _context.MedicalReports
                                 .Where(m => m.AppointmentId == appointmentId)
                                 .ToListAsync();
        }
        public async Task<List<MedicalReport>> GetMedicalReportsByDoctorIdAsync(Guid doctorId)
        {
            return await _context.MedicalReports
                                 .Where(r => r.Appointment.DoctorId == doctorId)
                                 .ToListAsync();
        }

    }
}
