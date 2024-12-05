using Entities.Enum;

namespace Entities.Concrete
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public AppointmentStatus AppointmentStatus{ get; set; }

        public Guid DoctorId { get; set; }
        public virtual User Doctor { get; set; }

        public Guid PatientId { get; set; }
        public virtual User Patient { get; set; }

        public virtual ICollection<MedicalReport> MedicalReports { get; set; } 

    }

}
