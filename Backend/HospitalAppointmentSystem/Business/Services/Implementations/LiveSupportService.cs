using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using DataAccess.UnitOfWork;
using DTO.LiveSupportDTOs;
using Entities.Concrete;

namespace Business.Services.Implementations
{
    public class LiveSupportService : ILiveSupportService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LiveSupportService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponseHelper<LiveSupportMessageDTO>> SendMessage(LiveSupportMessageDTO liveSupportMessageDto, Guid userId)
        {
            var liveSupportMessage = _mapper.Map<LiveSupport>(liveSupportMessageDto);

            liveSupportMessage.UserId = userId;  // UserId'yi doğrudan ayarlayın

            // Yeni mesaj oluştur ve MessageContent alanına ekleyin
            var newMessage = $"{liveSupportMessageDto.MessageContent}";
            if (string.IsNullOrEmpty(liveSupportMessage.MessageContent))
            {
                liveSupportMessage.MessageContent = newMessage;
            }
            else
            {
                liveSupportMessage.MessageContent += Environment.NewLine + newMessage;
            }

            await _unitOfWork.LiveSupports.AddAsync(liveSupportMessage);
            await _unitOfWork.CompleteAsync();

            return ServiceResponseHelper<LiveSupportMessageDTO>.SuccessResponse(liveSupportMessageDto, "Message sent successfully.");
        }


    }
}
