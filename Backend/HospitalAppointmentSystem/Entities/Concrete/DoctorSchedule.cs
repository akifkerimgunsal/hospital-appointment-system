
namespace Entities.Concrete
{
    public class DoctorSchedule : BaseEntity
    {
        public DateTime WorkDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Guid DoctorId { get; set; }
        public virtual User Doctor { get; set; }
    }

}
