using Entities.Concrete;
using Entities.Enum;

namespace DataAccess.Repositories.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<List<Appointment>> GetUpcomingAppointmentsByDoctorIdAsync(Guid doctorId);
        Task<List<Appointment>> GetPastAppointmentsByDoctorIdAsync(Guid doctorId);
        Task UpdateAppointmentStatusAsync(Guid appointmentId, AppointmentStatus status);
        Task CompleteAppointmentWithMedicalReportAsync(Guid appointmentId, MedicalReport report);
        Task<List<Appointment>> GetAppointmentsByPatientIdentityNumberAsync(string identityNumber);
        Task<List<Appointment>> GetPastAppointmentsByPatientIdAsync(Guid patientId);
        Task<List<Appointment>> GetAppointmentsByPatientIdAsync(Guid patientId);
    }
}
