using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<PasswordChange> PasswordChanges { get; set; }
        public ICollection<Appointment> AppointmentsAsPatient { get; set; }
        public ICollection<Appointment> AppointmentsAsDoctor { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
    }
}
