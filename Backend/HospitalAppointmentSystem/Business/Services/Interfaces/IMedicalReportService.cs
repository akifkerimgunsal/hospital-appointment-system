using Business.Helpers;
using DTO.MedicalReportDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IMedicalReportService
    {
        // Tıbbi Rapor Yönetimi
        Task<ServiceResponseHelper<MedicalReportDetailsDTO>> CreateMedicalReport(CreateMedicalReportDTO reportDto);
        Task<ServiceResponseHelper<List<MedicalReportSummaryDTO>>> GetMedicalReportsByAppointmentId(Guid appointmentId);
    }
}
