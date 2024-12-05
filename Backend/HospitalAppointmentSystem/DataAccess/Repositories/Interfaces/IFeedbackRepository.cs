using Entities.Concrete;

namespace DataAccess.Repositories.Interfaces
{
    public interface IFeedbackRepository : IBaseRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetFeedbacksByUserAsync(Guid userId);
        Task MarkFeedbackAsResolvedAsync(Guid feedbackId);
        Task ReplyToFeedbackAsync(Guid feedbackId, string response);
    }
}
