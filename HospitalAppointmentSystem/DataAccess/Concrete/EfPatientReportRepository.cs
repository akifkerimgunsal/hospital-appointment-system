using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfPatientReportRepository : IPatientReportRepository
    {
        private readonly HospitalContext _context;

        public EfPatientReportRepository(HospitalContext context)
        {
            _context = context;
        }

        public PatientReport Get(int id)
        {
            return _context.PatientReports.Find(id);
        }

        public List<PatientReport> GetAll()
        {
            return _context.PatientReports.ToList();
        }

        public void Add(PatientReport patientReport)
        {
            _context.PatientReports.Add(patientReport);
            _context.SaveChanges();
        }

        public void Update(PatientReport patientReport)
        {
            _context.PatientReports.Update(patientReport);
            _context.SaveChanges();
        }

        public void Delete(PatientReport patientReport)
        {
            _context.PatientReports.Remove(patientReport);
            _context.SaveChanges();
        }
    }
}
