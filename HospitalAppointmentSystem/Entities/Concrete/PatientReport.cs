using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PatientReport
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int AppointmentID { get; set; }
        public string ReportDetails { get; set; }

        public User Patient { get; set; }
        public User Doctor { get; set; }
        public Appointment Appointment { get; set; }
    }
}
