using Microsoft.AspNetCore.Mvc;
using DTO.AppointmentDTOs;
using DTO.FeedbackDTOs;
using Microsoft.AspNetCore.Authorization;
using Business.Services.Interfaces;
using HospitalAppointmentSystem.Attributes;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // Randevu Oluşturma
        [HttpPost("appointments")]
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDTO appointmentDto)
        {
            var response = await _patientService.CreateAppointment(appointmentDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Geçmiş Randevuları Görüntüleme
        [HttpGet("{patientId}/appointments/past")]
        public async Task<IActionResult> GetPastAppointments(Guid patientId)
        {
            var response = await _patientService.GetPastAppointments(patientId);
            return Ok(response);
        }

        // Gelecek Randevuları Görüntüleme
        [HttpGet("{patientId}/appointments/upcoming")]
        public async Task<IActionResult> GetUpcomingAppointments(Guid patientId)
        {
            var response = await _patientService.GetUpcomingAppointments(patientId);
            return Ok(response);
        }

        // Geri Bildirim Gönderme
        [HttpPost("feedbacks")]
        public async Task<IActionResult> SendFeedback([FromBody] CreateFeedbackDTO feedbackDto)
        {
            var response = await _patientService.SendFeedback(feedbackDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Doktorların Müsait Tarihlerini Görüntüleme
        [HttpGet("doctors/{doctorId}/available-dates")]
        public async Task<IActionResult> GetDoctorAvailableDates(Guid doctorId)
        {
            var response = await _patientService.GetDoctorAvailableDates(doctorId);
            return Ok(response);
        }
    }
}
