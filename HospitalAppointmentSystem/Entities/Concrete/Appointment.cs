using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Appointment
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public bool NotificationSent { get; set; }

        public User Patient { get; set; }
        public User Doctor { get; set; }
        public ICollection<PatientReport> PatientReports { get; set; }
    }
}
