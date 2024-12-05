using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using DataAccess.UnitOfWork;
using DTO.AppointmentDTOs;
using Entities.Concrete;

namespace Business.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Randevu Oluşturma
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

        // Randevu Güncelleme
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

        // Randevu İptali
        public async Task<ServiceResponseHelper<bool>> CancelAppointment(Guid appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment", appointmentId);

            _unitOfWork.Appointments.Remove(appointment);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<bool>.SuccessResponse(true, "Appointment canceled successfully.");
        }

        // Hasta ID'ye Göre Gelecek Randevuları Getir
        public async Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetUpcomingAppointmentsByPatientId(Guid patientId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(patientId);
            var upcomingAppointments = appointments.Where(a => a.AppointmentDate >= DateTime.Now).ToList();
            var appointmentDtos = _mapper.Map<List<AppointmentSummaryDTO>>(upcomingAppointments);

            return ServiceResponseHelper<List<AppointmentSummaryDTO>>.SuccessResponse(appointmentDtos, "Upcoming appointments retrieved successfully.");
        }

        // Hasta ID'ye Göre Geçmiş Randevuları Getir
        public async Task<ServiceResponseHelper<List<AppointmentSummaryDTO>>> GetPastAppointmentsByPatientId(Guid patientId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(patientId);
            var pastAppointments = appointments.Where(a => a.AppointmentDate < DateTime.Now).ToList();
            var appointmentDtos = _mapper.Map<List<AppointmentSummaryDTO>>(pastAppointments);

            return ServiceResponseHelper<List<AppointmentSummaryDTO>>.SuccessResponse(appointmentDtos, "Past appointments retrieved successfully.");
        }
    }
}
