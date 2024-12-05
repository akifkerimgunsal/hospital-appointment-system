using Business.Helpers;
using DTO.AppointmentDTOs;
using DTO.DoctorDTOs;
using DTO.MedicalReportDTOs;

namespace Business.Services.Interfaces
{
    public interface IDoctorService
    {
        // Doktor Çalışma Takvimi Yönetimi
        Task<ServiceResponseHelper<List<DoctorScheduleDTO>>> AddDoctorSchedules(Guid doctorId, AddDoctorScheduleDTO scheduleDto);
        Task<ServiceResponseHelper<List<DoctorScheduleDTO>>> GetDoctorSchedule(Guid doctorId);
        Task<ServiceResponseHelper<DoctorScheduleDTO>> UpdateDoctorSchedule(DoctorScheduleDTO scheduleDto);
        Task<ServiceResponseHelper<bool>> DeleteDoctorSchedule(Guid scheduleId);

        // Randevu Yönetimi
        Task<ServiceResponseHelper<List<AppointmentDetailsDTO>>> GetUpcomingAppointments(Guid doctorId);
        Task<ServiceResponseHelper<List<AppointmentDetailsDTO>>> GetPastAppointments(Guid doctorId);

        // Rapor İşlemleri
        Task<ServiceResponseHelper<MedicalReportDetailsDTO>> CreateMedicalReport(CreateMedicalReportDTO reportDto);
        Task<ServiceResponseHelper<List<MedicalReportSummaryDTO>>> GetMedicalReports(Guid doctorId);
    }
}
