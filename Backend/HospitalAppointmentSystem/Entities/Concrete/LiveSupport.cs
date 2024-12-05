
namespace Entities.Concrete
{
    public class LiveSupport : BaseEntity
    {
        public string MessageContent { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

    }

}
