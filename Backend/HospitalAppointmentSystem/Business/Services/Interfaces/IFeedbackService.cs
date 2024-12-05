using Business.Helpers;
using DTO.FeedbackDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IFeedbackService
    {
        // Geri Bildirim Gönderme
        Task<ServiceResponseHelper<FeedbackDetailsDTO>> SendFeedback(CreateFeedbackDTO feedbackDto);

        // Geri Bildirimleri Görüntüleme
        Task<ServiceResponseHelper<List<FeedbackDetailsDTO>>> GetAllFeedbacks();
        Task<ServiceResponseHelper<FeedbackDetailsDTO>> ReplyToFeedback(Guid feedbackId, string response);

    }
}
