using Business.Helpers;
using DTO.AppointmentDTOs;
using DTO.DoctorDTOs;
using DTO.FeedbackDTOs;
using Entities.Enum;

namespace Business.Services.Interfaces
{
    public interface IPatientService
    {
        // Randevu Yönetimi
        Task<ServiceResponseHelper<AppointmentDetailsDTO>> CreateAppointment(CreateAppointmentDTO appointmentDto);
        Task<ServiceResponseHelper<AppointmentDetailsDTO>> UpdateAppointment(UpdateAppointmentDTO appointmentDto);
        Task<ServiceResponseHelper<bool>> CancelAppointment(Guid appointmentId);
        Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetUpcomingAppointments(Guid patientId);
        Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetPastAppointments(Guid patientId);

        // Geri Bildirim
        Task<ServiceResponseHelper<FeedbackDetailsDTO>> SendFeedback(CreateFeedbackDTO feedbackDto);

        // Doktor Listeleme
        Task<ServiceResponseHelper<List<DoctorProfileDTO>>> GetDoctorsBySpecialty(DoctorSpecialty specialty);
        Task<ServiceResponseHelper<List<DateTime>>> GetDoctorAvailableDates(Guid doctorId);
    }
}
