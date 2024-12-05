using Business.Services.Interfaces;
using DTO.AdminDTOs;
using DTO.UserDTOs;
using DTO.DoctorDTOs;
using DTO.AppointmentDTOs;
using DTO.FeedbackDTOs;
using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using DataAccess.UnitOfWork;
using Entities.Enum;

namespace Business.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponseHelper<UserProfileDTO>> GetUserByIdentity(string identityNumber)
        {
            var user = await _unitOfWork.Users.GetUserByIdentityAsync(identityNumber);
            if (user == null)
                throw new NotFoundException("User", identityNumber);

            var userDto = _mapper.Map<UserProfileDTO>(user);
            return ServiceResponseHelper<UserProfileDTO>.SuccessResponse(userDto, "User found.");
        }

        public async Task<ServiceResponseHelper<List<UserProfileDTO>>> GetAllUsers()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var userDtos = _mapper.Map<List<UserProfileDTO>>(users);
            return ServiceResponseHelper<List<UserProfileDTO>>.SuccessResponse(userDtos, "Users retrieved successfully.");
        }

        public async Task<ServiceResponseHelper<UserProfileDTO>> UpdateUser(UserProfileDTO userDto)
        {
            var user = await _unitOfWork.Users.GetUserByIdentityAsync(userDto.IdentityNumber);
            if (user == null)
                throw new NotFoundException("User", userDto.IdentityNumber);

            _mapper.Map(userDto, user);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<UserProfileDTO>.SuccessResponse(userDto, "User updated successfully.");
        }

        public async Task<ServiceResponseHelper<bool>> DeleteUser(Guid userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<bool>.SuccessResponse(true, "User deleted successfully.");
        }

        public async Task<ServiceResponseHelper<bool>> SendNotificationToUser(Guid userId, string message)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            NotificationHelper.SendNotification(user.Email, message, true);
            NotificationHelper.SendNotification(user.PhoneNumber, message, false);

            return ServiceResponseHelper<bool>.SuccessResponse(true, "Notification sent successfully.");
        }

        public async Task<ServiceResponseHelper<List<DoctorScheduleDTO>>> GetDoctorSchedule(Guid doctorId)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetAllByIdAsync(s => s.DoctorId == doctorId);
            var scheduleDtos = _mapper.Map<List<DoctorScheduleDTO>>(schedule);
            return ServiceResponseHelper<List<DoctorScheduleDTO>>.SuccessResponse(scheduleDtos, "Doctor schedule retrieved successfully.");
        }

        public async Task<ServiceResponseHelper<DoctorScheduleDTO>> UpdateDoctorSchedule(DoctorScheduleDTO scheduleDto)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(scheduleDto.DoctorScheduleId);
            if (schedule == null)
                throw new NotFoundException("Doctor Schedule", scheduleDto.DoctorScheduleId);

            _mapper.Map(scheduleDto, schedule);
            _unitOfWork.DoctorSchedules.Update(schedule);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<DoctorScheduleDTO>.SuccessResponse(scheduleDto, "Doctor schedule updated successfully.");
        }

        public async Task<ServiceResponseHelper<bool>> DeleteDoctorSchedule(Guid scheduleId)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new NotFoundException("Doctor Schedule", scheduleId);

            _unitOfWork.DoctorSchedules.Remove(schedule);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<bool>.SuccessResponse(true, "Doctor schedule deleted successfully.");
        }

        public async Task<ServiceResponseHelper<List<AppointmentDetailsDTO>>> GetPatientAppointmentsByIdentity(string identityNumber)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientIdentityNumberAsync(identityNumber);
            var appointmentDtos = _mapper.Map<List<AppointmentDetailsDTO>>(appointments);
            return ServiceResponseHelper<List<AppointmentDetailsDTO>>.SuccessResponse(appointmentDtos, "Appointments retrieved successfully.");
        }

        public async Task<ServiceResponseHelper<UpdateAppointmentDTO>> UpdatePatientAppointment(UpdateAppointmentDTO appointmentDto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentDto.AppointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment", appointmentDto.AppointmentId);

            _mapper.Map(appointmentDto, appointment);
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<UpdateAppointmentDTO>.SuccessResponse(appointmentDto, "Appointment updated successfully.");
        }

        public async Task<ServiceResponseHelper<bool>> DeletePatientAppointment(Guid appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment", appointmentId);

            _unitOfWork.Appointments.Remove(appointment);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<bool>.SuccessResponse(true, "Appointment deleted successfully.");
        }

        public async Task<ServiceResponseHelper<List<FeedbackDetailsDTO>>> GetAllFeedbacks()
        {
            var feedbacks = await _unitOfWork.Feedbacks.GetAllAsync();
            var feedbackDtos = _mapper.Map<List<FeedbackDetailsDTO>>(feedbacks);
            return ServiceResponseHelper<List<FeedbackDetailsDTO>>.SuccessResponse(feedbackDtos, "Feedbacks retrieved successfully.");
        }

        public async Task<ServiceResponseHelper<FeedbackDetailsDTO>> ReplyToFeedback(Guid feedbackId, string response)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(feedbackId);
            if (feedback == null)
                throw new NotFoundException("Feedback", feedbackId);

            feedback.AdminResponse = response;
            feedback.IsResolved = true;
            _unitOfWork.Feedbacks.Update(feedback);
            await _unitOfWork.CompleteAsync();

            var feedbackDto = _mapper.Map<FeedbackDetailsDTO>(feedback);
            return ServiceResponseHelper<FeedbackDetailsDTO>.SuccessResponse(feedbackDto, "Feedback replied successfully.");
        }

        public async Task<ServiceResponseHelper<bool>> DeleteFeedback(Guid feedbackId)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(feedbackId);
            if (feedback == null)
                throw new NotFoundException("Feedback", feedbackId);

            _unitOfWork.Feedbacks.Remove(feedback);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<bool>.SuccessResponse(true, "Feedback deleted successfully.");
        }

        public async Task<ServiceResponseHelper<AdminReportDTO>> GetSystemReports()
        {
            var report = new AdminReportDTO
            {
                TotalUsers = await _unitOfWork.Users.CountAllAsync(),
                TotalDoctors = (await _unitOfWork.Users.GetUsersByRoleAsync(UserRole.Doctor)).Count(),
                TotalPatients = (await _unitOfWork.Users.GetUsersByRoleAsync(UserRole.Patient)).Count(),
                TotalAppointments = await _unitOfWork.Appointments.CountAllAsync()
            };
            return ServiceResponseHelper<AdminReportDTO>.SuccessResponse(report, "System report generated successfully.");
        }

        public async Task<ServiceResponseHelper<bool>> DeleteUserByIdentityNumber(string identityNumber)
        {
            var user = await _unitOfWork.Users.GetUserByIdentityNumberAsync(identityNumber);
            if (user == null)
            {
                return ServiceResponseHelper<bool>.ErrorResponse("User not found.");
            }

            user.IsDeleted = true;  // Soft delete işlemi
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<bool>.SuccessResponse(true, "User soft deleted successfully.");
        }


    }
}
