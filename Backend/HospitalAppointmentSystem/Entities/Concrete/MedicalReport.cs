
namespace Entities.Concrete
{
    public class MedicalReport : BaseEntity
    {
        public string MedicalReportContent { get; set; }

        public Guid AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
    }

}
