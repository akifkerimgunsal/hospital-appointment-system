using Entities.Concrete;

namespace DataAccess.Repositories.Interfaces
{
    public interface IMedicalReportRepository : IBaseRepository<MedicalReport>
    {
        Task<IEnumerable<MedicalReport>> GetMedicalReportsByAppointmentAsync(Guid appointmentId);
        Task SubmitMedicalReportAsync(MedicalReport report);
        Task<List<MedicalReport>> GetMedicalReportsByAppointmentIdAsync(Guid appointmentId);
        Task<List<MedicalReport>> GetMedicalReportsByDoctorIdAsync(Guid doctorId);
    }
}
