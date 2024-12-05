using Entities.Enum;

namespace Entities.Concrete
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public DoctorSpecialty DoctorSpecialty { get; set; }


        public virtual ICollection<Feedback>? Feedbacks { get; set; }
        public virtual ICollection<LiveSupport>? LiveSupports { get; set; } 
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<DoctorSchedule> Schedules { get; set; }


        public virtual ICollection<Appointment> DoctorAppointments { get; set; }
        public virtual ICollection<Appointment> PatientAppointments { get; set; }


    }
}