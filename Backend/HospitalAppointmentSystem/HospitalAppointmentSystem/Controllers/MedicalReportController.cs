using Microsoft.AspNetCore.Mvc;
using DTO.MedicalReportDTOs;
using Microsoft.AspNetCore.Authorization;
using Business.Services.Interfaces;
using HospitalAppointmentSystem.Attributes;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicalReportController : ControllerBase
    {
        private readonly IMedicalReportService _medicalReportService;

        public MedicalReportController(IMedicalReportService medicalReportService)
        {
            _medicalReportService = medicalReportService;
        }

        // Tıbbi Rapor Oluşturma
        [HttpPost("{appointmentId}/create")]
        public async Task<IActionResult> CreateMedicalReport(Guid appointmentId, [FromBody] CreateMedicalReportDTO reportDto)
        {
            reportDto.AppointmentId = appointmentId;
            var response = await _medicalReportService.CreateMedicalReport(reportDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Tıbbi Raporları Görüntüleme
        [HttpGet("{appointmentId}/reports")]
        public async Task<IActionResult> GetMedicalReports(Guid appointmentId)
        {
            var response = await _medicalReportService.GetMedicalReportsByAppointmentId(appointmentId);
            return Ok(response);
        }
    }
}
