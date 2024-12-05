using Microsoft.AspNetCore.Mvc;
using DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Business.Services.Interfaces;
using HospitalAppointmentSystem.Attributes;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Kullanıcıları Görüntüleme
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _adminService.GetAllUsers();
            return Ok(response);
        }

        // Belirli Bir Kullanıcıyı Görüntüleme
        [HttpGet("users/{identityNumber}")]
        public async Task<IActionResult> GetUserByIdentity(string identityNumber)
        {
            var response = await _adminService.GetUserByIdentity(identityNumber);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Kullanıcı Güncelleme
        [HttpPut("users")]
        public async Task<IActionResult> UpdateUser([FromBody] UserProfileDTO userDto)
        {
            var response = await _adminService.UpdateUser(userDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        //// Kullanıcı Silme
        //[HttpDelete("users/{userId}")]
        //public async Task<IActionResult> DeleteUser(Guid userId)
        //{
        //    var response = await _adminService.DeleteUser(userId);
        //    if (!response.Success)
        //    {
        //        return NotFound(response);
        //    }
        //    return Ok(response);
        //}

        [HttpDelete("users/delete/{identityNumber}")]
        public async Task<IActionResult> DeleteUserByIdentityNumber(string identityNumber)
        {
            var response = await _adminService.DeleteUserByIdentityNumber(identityNumber);

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Kullanıcıya Bildirim Gönderme
        [HttpPost("users/{userId}/send-notification")]
        public async Task<IActionResult> SendUserNotification(Guid userId, [FromBody] string message)
        {
            var response = await _adminService.SendNotificationToUser(userId, message);
            return Ok(response);
        }

        // Hasta ve Doktora Bildirim Gönderme
        [HttpPost("patients/{patientId}/send-notification")]
        public async Task<IActionResult> SendPatientNotification(Guid patientId, [FromBody] string message)
        {
            var response = await _adminService.SendNotificationToUser(patientId, message);
            return Ok(response);
        }

        [HttpPost("doctors/{doctorId}/send-notification")]
        public async Task<IActionResult> SendDoctorNotification(Guid doctorId, [FromBody] string message)
        {
            var response = await _adminService.SendNotificationToUser(doctorId, message);
            return Ok(response);
        }

        // Doktor Çalışma Takvimi Görüntüleme
        [HttpGet("doctors/{doctorId}/schedule")]
        public async Task<IActionResult> GetDoctorSchedule(Guid doctorId)
        {
            var response = await _adminService.GetDoctorSchedule(doctorId);
            return Ok(response);
        }

        // Hasta Randevularını Görüntüleme
        [HttpGet("patients/{identityNumber}/appointments")]
        public async Task<IActionResult> GetPatientAppointmentsByIdentity(string identityNumber)
        {
            var response = await _adminService.GetPatientAppointmentsByIdentity(identityNumber);
            return Ok(response);
        }

        // Geri Bildirimleri Görüntüleme
        [HttpGet("feedbacks")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var response = await _adminService.GetAllFeedbacks();
            return Ok(response);
        }

        // Geri Bildirime Yanıt Verme
        [HttpPut("feedbacks/{feedbackId}")]
        public async Task<IActionResult> ReplyToFeedback(Guid feedbackId, [FromBody] string responseContent)
        {
            var response = await _adminService.ReplyToFeedback(feedbackId, responseContent);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Sistem Raporları Görüntüleme
        [HttpGet("reports")]
        public async Task<IActionResult> GetSystemReports()
        {
            var response = await _adminService.GetSystemReports();
            return Ok(response);
        }
    }
}
