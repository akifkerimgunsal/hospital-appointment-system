using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfAppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalContext _context;

        public EfAppointmentRepository(HospitalContext context)
        {
            _context = context;
        }

        public Appointment Get(int id)
        {
            return _context.Appointments.Find(id);
        }

        public List<Appointment> GetAll()
        {
            return _context.Appointments.ToList();
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void Delete(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }
    }
}
