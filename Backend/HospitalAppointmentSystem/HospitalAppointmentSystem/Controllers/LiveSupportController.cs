using Microsoft.AspNetCore.Mvc;
using DTO.LiveSupportDTOs;
using Microsoft.AspNetCore.Authorization;
using Business.Services.Interfaces;
using System.Security.Claims;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LiveSupportController : ControllerBase
    {
        private readonly ILiveSupportService _liveSupportService;

        public LiveSupportController(ILiveSupportService liveSupportService)
        {
            _liveSupportService = liveSupportService;
        }

        [HttpPost("live-support")]
        public async Task<IActionResult> SendMessage([FromBody] LiveSupportMessageDTO messageDto)
        {
            // Kullanıcı ID'sini Claims içinde güvenle alın
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return BadRequest(new { error = "User ID claim not found in token." });
            }

            // Claim değeri varsa UserId'yi Guid olarak parse edin
            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest(new { error = "Invalid User ID format." });
            }

            var response = await _liveSupportService.SendMessage(messageDto, userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }




    }
}
