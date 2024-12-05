using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities.Concrete;
using Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId)
        {
            return await _context.Appointments
                                 .Where(a => a.DoctorId == doctorId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId)
        {
            return await _context.Appointments
                                 .Where(a => a.PatientId == patientId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _context.Appointments
                                 .Where(a => a.AppointmentDate.Date == date.Date)
                                 .ToListAsync();
        }
        public async Task<List<Appointment>> GetUpcomingAppointmentsByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Appointments
                                 .Where(a => a.DoctorId == doctorId && a.AppointmentDate >= DateTime.Now)
                                 .ToListAsync();
        }
        public async Task<List<Appointment>> GetPastAppointmentsByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Appointments
                                 .Where(a => a.DoctorId == doctorId && a.AppointmentDate < DateTime.Now)
                                 .ToListAsync();
        }


        public async Task UpdateAppointmentStatusAsync(Guid appointmentId, AppointmentStatus status)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.AppointmentStatus = status;
                _context.Appointments.Update(appointment);
            }
        }

        public async Task CompleteAppointmentWithMedicalReportAsync(Guid appointmentId, MedicalReport report)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);

            if (appointment != null)
            {
                appointment.AppointmentStatus = AppointmentStatus.Completed;
                appointment.MedicalReports.Add(report);
                _context.Appointments.Update(appointment);
            }
        }
        public async Task<List<Appointment>> GetAppointmentsByPatientIdentityNumberAsync(string identityNumber)
        {
            return await _context.Appointments
                                 .Include(a => a.Patient)
                                 .Where(a => a.Patient.IdentityNumber == identityNumber)
                                 .ToListAsync();
        }
        public async Task<List<Appointment>> GetPastAppointmentsByPatientIdAsync(Guid patientId)
        {
            return await _context.Appointments
                                 .Include(a => a.Doctor)
                                 .Where(a => a.PatientId == patientId && a.AppointmentDate < DateTime.Now)
                                 .ToListAsync();
        }
        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(Guid patientId)
        {
            return await _context.Appointments
                                 .Where(a => a.PatientId == patientId)
                                 .ToListAsync();
        }
    }
}
