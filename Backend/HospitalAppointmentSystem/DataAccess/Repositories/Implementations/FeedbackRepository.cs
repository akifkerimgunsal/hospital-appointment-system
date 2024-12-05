using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserAsync(Guid userId)
        {
            return await _context.Feedbacks
                                 .Where(f => f.UserId == userId)
                                 .ToListAsync();
        }

        public async Task MarkFeedbackAsResolvedAsync(Guid feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback != null)
            {
                feedback.IsResolved = true;
                _context.Feedbacks.Update(feedback);
            }
        }

        public async Task ReplyToFeedbackAsync(Guid feedbackId, string response)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback != null)
            {
                feedback.AdminResponse = response;
                _context.Feedbacks.Update(feedback);
            }
        }
    }
}
