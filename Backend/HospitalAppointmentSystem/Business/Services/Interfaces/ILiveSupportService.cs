using Business.Helpers;
using DTO.LiveSupportDTOs;

namespace Business.Services.Interfaces
{
    public interface ILiveSupportService
    {
        // Canlı Destek Mesaj Gönderme
        Task<ServiceResponseHelper<LiveSupportMessageDTO>> SendMessage(LiveSupportMessageDTO liveSupportMessageDto, Guid userId);
    }
}
