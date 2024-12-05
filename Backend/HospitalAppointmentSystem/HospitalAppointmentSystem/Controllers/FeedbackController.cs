using Microsoft.AspNetCore.Mvc;
using DTO.FeedbackDTOs;
using Microsoft.AspNetCore.Authorization;
using Business.Services.Interfaces;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // Kullanıcı Geri Bildirim Gönderme
        [HttpPost]
        public async Task<IActionResult> SendFeedback([FromBody] CreateFeedbackDTO feedbackDto)
        {
            var response = await _feedbackService.SendFeedback(feedbackDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
