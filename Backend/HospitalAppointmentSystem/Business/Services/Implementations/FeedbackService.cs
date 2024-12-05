using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using DataAccess.UnitOfWork;
using DTO.FeedbackDTOs;
using Entities.Concrete;

namespace Business.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Geri Bildirim Gönderme
        public async Task<ServiceResponseHelper<FeedbackDetailsDTO>> SendFeedback(CreateFeedbackDTO feedbackDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(feedbackDto.UserId);
            if (user == null)
                throw new NotFoundException("User", feedbackDto.UserId);

            var feedback = _mapper.Map<Feedback>(feedbackDto);
            await _unitOfWork.Feedbacks.AddAsync(feedback);
            await _unitOfWork.CompleteAsync();

            var feedbackDetailsDto = _mapper.Map<FeedbackDetailsDTO>(feedback);
            return ServiceResponseHelper<FeedbackDetailsDTO>.SuccessResponse(feedbackDetailsDto, "Feedback sent successfully.");
        }

        // Tüm Geri Bildirimleri Görüntüleme
        public async Task<ServiceResponseHelper<List<FeedbackDetailsDTO>>> GetAllFeedbacks()
        {
            var feedbacks = await _unitOfWork.Feedbacks.GetAllAsync();
            var feedbackDtos = _mapper.Map<List<FeedbackDetailsDTO>>(feedbacks);

            return ServiceResponseHelper<List<FeedbackDetailsDTO>>.SuccessResponse(feedbackDtos, "All feedbacks retrieved successfully.");
        }
        public async Task<ServiceResponseHelper<FeedbackDetailsDTO>> ReplyToFeedback(Guid feedbackId, string response)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(feedbackId);
            if (feedback == null)
                throw new NotFoundException("Feedback", feedbackId);

            feedback.AdminResponse = response;
            feedback.IsResolved = true;
            _unitOfWork.Feedbacks.Update(feedback);
            await _unitOfWork.CompleteAsync();

            var feedbackDto = _mapper.Map<FeedbackDetailsDTO>(feedback);
            return ServiceResponseHelper<FeedbackDetailsDTO>.SuccessResponse(feedbackDto, "Feedback replied successfully.");
        }
    }
}
