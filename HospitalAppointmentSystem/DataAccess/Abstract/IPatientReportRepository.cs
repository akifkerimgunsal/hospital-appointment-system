using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IPatientReportRepository
    {
        PatientReport Get(int id);
        List<PatientReport> GetAll();
        void Add(PatientReport patientReport);
        void Update(PatientReport patientReport);
        void Delete(PatientReport patientReport);
    }
}
