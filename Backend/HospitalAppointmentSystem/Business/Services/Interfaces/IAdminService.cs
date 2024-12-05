using DTO.AdminDTOs;
using DTO.UserDTOs;
using DTO.DoctorDTOs;
using DTO.AppointmentDTOs;
using DTO.FeedbackDTOs;
using Business.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Business.Services.Interfaces
{
    public interface IAdminService
    {
        // Kullanıcı Yönetimi
        Task<ServiceResponseHelper<UserProfileDTO>> GetUserByIdentity(string identityNumber);
        Task<ServiceResponseHelper<List<UserProfileDTO>>> GetAllUsers();
        Task<ServiceResponseHelper<UserProfileDTO>> UpdateUser(UserProfileDTO userDto);
        Task<ServiceResponseHelper<bool>> DeleteUser(Guid userId);
        Task<ServiceResponseHelper<bool>> SendNotificationToUser(Guid userId, string message);
        Task<ServiceResponseHelper<bool>> DeleteUserByIdentityNumber(string identityNumber);

        // Doktor Takvim Yönetimi
        Task<ServiceResponseHelper<List<DoctorScheduleDTO>>> GetDoctorSchedule(Guid doctorId);
        Task<ServiceResponseHelper<DoctorScheduleDTO>> UpdateDoctorSchedule(DoctorScheduleDTO scheduleDto);
        Task<ServiceResponseHelper<bool>> DeleteDoctorSchedule(Guid scheduleId);
        
        // Hasta Randevu Yönetimi
        Task<ServiceResponseHelper<List<AppointmentDetailsDTO>>> GetPatientAppointmentsByIdentity(string identityNumber);
        Task<ServiceResponseHelper<UpdateAppointmentDTO>> UpdatePatientAppointment(UpdateAppointmentDTO appointmentDto);
        Task<ServiceResponseHelper<bool>> DeletePatientAppointment(Guid appointmentId);

        // Geri Bildirim Yönetimi
        Task<ServiceResponseHelper<List<FeedbackDetailsDTO>>> GetAllFeedbacks();
        Task<ServiceResponseHelper<FeedbackDetailsDTO>> ReplyToFeedback(Guid feedbackId, string response);
        Task<ServiceResponseHelper<bool>> DeleteFeedback(Guid feedbackId);

        // Sistem Raporları
        Task<ServiceResponseHelper<AdminReportDTO>> GetSystemReports();
    }
}
