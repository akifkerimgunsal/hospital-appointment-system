using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using DataAccess.UnitOfWork;
using DTO.AppointmentDTOs;
using DTO.DoctorDTOs;
using DTO.MedicalReportDTOs;
using Entities.Concrete;

namespace Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Doktor Çalışma Takvimi Yönetimi
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

        // Randevu Yönetimi
        public async Task<ServiceResponseHelper<List<AppointmentDetailsDTO>>> GetUpcomingAppointments(Guid doctorId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByDoctorAsync(doctorId);
            var appointmentDtos = _mapper.Map<List<AppointmentDetailsDTO>>(appointments);
            return ServiceResponseHelper<List<AppointmentDetailsDTO>>.SuccessResponse(appointmentDtos, "Upcoming appointments retrieved successfully.");
        }

        public async Task<ServiceResponseHelper<List<AppointmentDetailsDTO>>> GetPastAppointments(Guid doctorId)
        {
            var appointments = await _unitOfWork.Appointments.GetPastAppointmentsByDoctorIdAsync(doctorId);
            var appointmentDtos = _mapper.Map<List<AppointmentDetailsDTO>>(appointments);
            return ServiceResponseHelper<List<AppointmentDetailsDTO>>.SuccessResponse(appointmentDtos, "Past appointments retrieved successfully.");
        }

        // Rapor İşlemleri
        public async Task<ServiceResponseHelper<MedicalReportDetailsDTO>> CreateMedicalReport(CreateMedicalReportDTO reportDto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(reportDto.AppointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment", reportDto.AppointmentId);

            var medicalReport = _mapper.Map<MedicalReport>(reportDto);
            medicalReport.AppointmentId = reportDto.AppointmentId;

            await _unitOfWork.MedicalReports.AddAsync(medicalReport);
            await _unitOfWork.CompleteAsync();

            var reportDtoResult = _mapper.Map<MedicalReportDetailsDTO>(medicalReport);
            return ServiceResponseHelper<MedicalReportDetailsDTO>.SuccessResponse(reportDtoResult, "Medical report created successfully.");
        }

        public async Task<ServiceResponseHelper<List<MedicalReportSummaryDTO>>> GetMedicalReports(Guid doctorId)
        {
            var reports = await _unitOfWork.MedicalReports.GetMedicalReportsByDoctorIdAsync(doctorId);
            var reportDtos = _mapper.Map<List<MedicalReportSummaryDTO>>(reports);
            return ServiceResponseHelper<List<MedicalReportSummaryDTO>>.SuccessResponse(reportDtos, "Medical reports retrieved successfully.");
        }
        public async Task<ServiceResponseHelper<List<DoctorScheduleDTO>>> AddDoctorSchedules(Guid doctorId, AddDoctorScheduleDTO scheduleDto)
        {
            var workDaySchedules = new List<DoctorSchedule>();

            // Sabit saat aralıkları
            var startEndTimes = new List<(TimeSpan start, TimeSpan end)>
    {
        (TimeSpan.Parse("09:00:00"), TimeSpan.Parse("12:00:00")),
        (TimeSpan.Parse("13:30:00"), TimeSpan.Parse("17:00:00"))
    };

            foreach (var date in scheduleDto.WorkDays)
            {
                // Aynı gün için çalışma saati varsa atlayın
                var existingSchedule = await _unitOfWork.DoctorSchedules.GetDoctorScheduleByDateAsync(doctorId, date);
                if (existingSchedule != null) continue;

                // Her gün için sabit çalışma saatlerini ekle
                foreach (var (start, end) in startEndTimes)
                {
                    var newSchedule = new DoctorSchedule
                    {
                        DoctorId = doctorId,
                        WorkDay = date,
                        StartTime = start,
                        EndTime = end
                    };
                    workDaySchedules.Add(newSchedule);
                }
            }

            await _unitOfWork.DoctorSchedules.AddRangeAsync(workDaySchedules);
            await _unitOfWork.CompleteAsync();

            var scheduleResult = _mapper.Map<List<DoctorScheduleDTO>>(workDaySchedules);
            return ServiceResponseHelper<List<DoctorScheduleDTO>>.SuccessResponse(scheduleResult, "Doctor schedules added successfully.");
        }




    }
}
