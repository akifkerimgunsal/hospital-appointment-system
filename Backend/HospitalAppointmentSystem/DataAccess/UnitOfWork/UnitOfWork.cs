using Entities.Concrete;
using DataAccess.Context;
using DataAccess.Repositories.Interfaces;


namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IAppointmentRepository Appointments { get; private set; }
        public IUserRepository Users { get; private set; }
        public IFeedbackRepository Feedbacks { get; private set; }
        public IMedicalReportRepository MedicalReports { get; private set; }
        public IDoctorScheduleRepository DoctorSchedules { get; private set; }
        public ILiveSupportRepository LiveSupports { get; private set; }

        public UnitOfWork(AppDbContext context,
                      IAppointmentRepository appointments,
                      IUserRepository users,
                      IFeedbackRepository feedbacks,
                      IMedicalReportRepository medicalReports,
                      IDoctorScheduleRepository doctorSchedules,
                      ILiveSupportRepository liveSupports)
        {
            _context = context;
            Users = users;
            Appointments = appointments;
            DoctorSchedules = doctorSchedules;
            Feedbacks = feedbacks;
            MedicalReports = medicalReports;
            LiveSupports = liveSupports;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
