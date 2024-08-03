using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAppointmentRepository
    {
        Appointment Get(int id);
        List<Appointment> GetAll();
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
    }
}
