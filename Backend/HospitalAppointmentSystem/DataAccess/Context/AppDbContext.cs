using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<LiveSupport> LiveSupports { get; set; }
        public DbSet<MedicalReport> MedicalReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion<string>();
            modelBuilder.Entity<User>()
                .Property(u => u.DoctorSpecialty)
                .HasConversion<string>();

            // Doktor ile Randevular arasındaki ilişki
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.DoctorAppointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Hasta ile Randevular arasındaki ilişki
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.PatientAppointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        }


    }
}
