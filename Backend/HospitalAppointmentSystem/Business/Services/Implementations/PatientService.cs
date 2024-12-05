using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using DataAccess.UnitOfWork;
using DTO.AppointmentDTOs;
using DTO.DoctorDTOs;
using DTO.FeedbackDTOs;
using Entities.Enum;
using Entities.Concrete;

namespace Business.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Randevu Yönetimi
        public async Task<ServiceResponseHelper<AppointmentDetailsDTO>> CreateAppointment(CreateAppointmentDTO appointmentDto)
        {
            var doctor = await _unitOfWork.Users.GetByIdAsync(appointmentDto.DoctorId);
            if (doctor == null)
                throw new NotFoundException("Doctor", appointmentDto.DoctorId);

            var appointment = _mapper.Map<Appointment>(appointmentDto);
            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            var appointmentDetailsDto = _mapper.Map<AppointmentDetailsDTO>(appointment);
            return ServiceResponseHelper<AppointmentDetailsDTO>.SuccessResponse(appointmentDetailsDto, "Appointment created successfully.");
        }

        public async Task<ServiceResponseHelper<AppointmentDetailsDTO>> UpdateAppointment(UpdateAppointmentDTO appointmentDto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentDto.AppointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment", appointmentDto.AppointmentId);

            _mapper.Map(appointmentDto, appointment);
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.CompleteAsync();

            var updatedAppointmentDto = _mapper.Map<AppointmentDetailsDTO>(appointment);
            return ServiceResponseHelper<AppointmentDetailsDTO>.SuccessResponse(updatedAppointmentDto, "Appointment updated successfully.");
        }

        public async Task<ServiceResponseHelper<bool>> CancelAppointment(Guid appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment", appointmentId);

            _unitOfWork.Appointments.Remove(appointment);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<bool>.SuccessResponse(true, "Appointment canceled successfully.");
        }

        public async Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetUpcomingAppointments(Guid patientId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(patientId);
            var upcomingAppointments = appointments.Where(a => a.AppointmentDate >= DateTime.Now).ToList();
            var appointmentDtos = _mapper.Map<List<AppointmentSummaryDTO>>(upcomingAppointments);

            return ServiceResponseHelper<List<AppointmentSummaryDTO>>.SuccessResponse(appointmentDtos, "Upcoming appointments retrieved successfully.");
        }

        public async Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetPastAppointments(Guid patientId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(patientId);
            var pastAppointments = appointments.Where(a => a.AppointmentDate < DateTime.Now).ToList();
            var appointmentDtos = _mapper.Map<List<AppointmentSummaryDTO>>(pastAppointments);

            return ServiceResponseHelper<List<AppointmentSummaryDTO>>.SuccessResponse(appointmentDtos, "Past appointments retrieved successfully.");
        }

        // Geri Bildirim
        public async Task<ServiceResponseHelper<FeedbackDetailsDTO>> SendFeedback(CreateFeedbackDTO feedbackDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(feedbackDto.UserId);
            if (user == null)
                throw new NotFoundException("User", feedbackDto.UserId);

            var feedback = _mapper.Map<Feedback>(feedbackDto);
            await _unitOfWork.Feedbacks.AddAsync(feedback);
            await _unitOfWork.CompleteAsync();

            var feedbackDetailsDto = _mapper.Map<FeedbackDetailsDTO>(feedback);
            return ServiceResponseHelper<FeedbackDetailsDTO>.SuccessResponse(feedbackDetailsDto, "Feedback sent successfully.");
        }

        // Doktor Listeleme
        public async Task<ServiceResponseHelper<List<DoctorProfileDTO>>> GetDoctorsBySpecialty(DoctorSpecialty specialty)
        {
            var doctors = await _unitOfWork.Users.GetDoctorsBySpecialtyAsync(specialty);
            var doctorDtos = _mapper.Map<List<DoctorProfileDTO>>(doctors);
            return ServiceResponseHelper<List<DoctorProfileDTO>>.SuccessResponse(doctorDtos, "Doctors retrieved successfully.");
        }
        public async Task<ServiceResponseHelper<List<DateTime>>> GetDoctorAvailableDates(Guid doctorId)
        {
            var availableDates = await _unitOfWork.DoctorSchedules.GetAvailableDatesByDoctorIdAsync(doctorId);
            if (availableDates == null || !availableDates.Any())
            {
                return ServiceResponseHelper<List<DateTime>>.ErrorResponse("No available dates found.");
            }

            return ServiceResponseHelper<List<DateTime>>.SuccessResponse(availableDates, "Available dates retrieved successfully.");
        }
    }
}
