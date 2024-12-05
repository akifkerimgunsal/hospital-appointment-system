using Business.Helpers;
using DTO.AppointmentDTOs;

namespace Business.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<ServiceResponseHelper<AppointmentDetailsDTO>> CreateAppointment(CreateAppointmentDTO appointmentDto);
        Task<ServiceResponseHelper<AppointmentDetailsDTO>> UpdateAppointment(UpdateAppointmentDTO appointmentDto);
        Task<ServiceResponseHelper<bool>> CancelAppointment(Guid appointmentId);
        Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetUpcomingAppointmentsByPatientId(Guid patientId);
        Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetPastAppointmentsByPatientId(Guid patientId);
    }
}
