
namespace Entities.Concrete
{
    public class Feedback : BaseEntity
    {
        public string FeedbackContent { get; set; }
        public bool IsResolved { get; set; } = false;
        public string? AdminResponse { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }

}
