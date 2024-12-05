using Microsoft.AspNetCore.Mvc;
using DTO.AppointmentDTOs;
using DTO.DoctorDTOs;
using DTO.MedicalReportDTOs;
using Microsoft.AspNetCore.Authorization;
using Business.Services.Interfaces;
using HospitalAppointmentSystem.Attributes;
using System.Security.Claims;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AuthorizeRole("Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("schedules")]
        public async Task<IActionResult> AddDoctorSchedules([FromBody] AddDoctorScheduleDTO scheduleDto)
        {
            // Token içinden doctorId'yi alma
            var doctorIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (doctorIdClaim == null || !Guid.TryParse(doctorIdClaim.Value, out var doctorId))
            {
                return Unauthorized("Invalid doctor ID.");
            }

            var response = await _doctorService.AddDoctorSchedules(doctorId, scheduleDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Doktor Çalışma Takvimi Görüntüleme
        [HttpGet("{doctorId}/schedule")]
        public async Task<IActionResult> GetDoctorSchedule(Guid doctorId)
        {
            var response = await _doctorService.GetDoctorSchedule(doctorId);
            return Ok(response);
        }

        // Gelecek Randevuları Görüntüleme
        [HttpGet("{doctorId}/appointments/upcoming")]
        public async Task<IActionResult> GetUpcomingAppointments(Guid doctorId)
        {
            var response = await _doctorService.GetUpcomingAppointments(doctorId);
            return Ok(response);
        }

        // Geçmiş Randevuları Görüntüleme
        [HttpGet("{doctorId}/appointments/past")]
        public async Task<IActionResult> GetPastAppointments(Guid doctorId)
        {
            var response = await _doctorService.GetPastAppointments(doctorId);
            return Ok(response);
        }

        // Tıbbi Rapor Ekleme
        [HttpPost("appointments/{appointmentId}/medical-report")]
        public async Task<IActionResult> CreateMedicalReport(Guid appointmentId, [FromBody] CreateMedicalReportDTO reportDto)
        {
            reportDto.AppointmentId = appointmentId;
            var response = await _doctorService.CreateMedicalReport(reportDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Hastaların Tıbbi Raporlarını Görüntüleme
        [HttpGet("{doctorId}/medical-reports")]
        public async Task<IActionResult> GetMedicalReports(Guid doctorId)
        {
            var response = await _doctorService.GetMedicalReports(doctorId);
            return Ok(response);
        }
    }
}
