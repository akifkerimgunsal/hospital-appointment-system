using DataAccess.Repositories.Interfaces;
using Entities.Concrete;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAppointmentRepository Appointments { get; }
        IDoctorScheduleRepository DoctorSchedules { get; }
        IFeedbackRepository Feedbacks { get; }
        IMedicalReportRepository MedicalReports { get; }
        ILiveSupportRepository LiveSupports { get; }


        int Complete();
        Task<int> CompleteAsync();
    }

}
