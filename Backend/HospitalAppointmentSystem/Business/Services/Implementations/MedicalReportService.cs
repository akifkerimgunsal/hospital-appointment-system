using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using DataAccess.UnitOfWork;
using DTO.MedicalReportDTOs;
using Entities.Concrete;

namespace Business.Services.Implementations
{
    public class MedicalReportService : IMedicalReportService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MedicalReportService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Tıbbi Rapor Oluşturma
        public async Task<ServiceResponseHelper<MedicalReportDetailsDTO>> CreateMedicalReport(CreateMedicalReportDTO reportDto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(reportDto.AppointmentId);
            if (appointment == null)
                throw new NotFoundException("Appointment", reportDto.AppointmentId);

            var medicalReport = _mapper.Map<MedicalReport>(reportDto);
            medicalReport.AppointmentId = reportDto.AppointmentId;

            await _unitOfWork.MedicalReports.AddAsync(medicalReport);
            await _unitOfWork.CompleteAsync();

            var reportDetailsDto = _mapper.Map<MedicalReportDetailsDTO>(medicalReport);
            return ServiceResponseHelper<MedicalReportDetailsDTO>.SuccessResponse(reportDetailsDto, "Medical report created successfully.");
        }

        // Randevu ID'ye Göre Tıbbi Raporları Getir
        public async Task<ServiceResponseHelper<List<MedicalReportSummaryDTO>>> GetMedicalReportsByAppointmentId(Guid appointmentId)
        {
            var reports = await _unitOfWork.MedicalReports.GetMedicalReportsByAppointmentIdAsync(appointmentId);
            var reportDtos = _mapper.Map<List<MedicalReportSummaryDTO>>(reports);

            return ServiceResponseHelper<List<MedicalReportSummaryDTO>>.SuccessResponse(reportDtos, "Medical reports retrieved successfully.");
        }
    }
}
