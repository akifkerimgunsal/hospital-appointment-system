using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfNotificationRepository : INotificationRepository
    {
        private readonly HospitalContext _context;

        public EfNotificationRepository(HospitalContext context)
        {
            _context = context;
        }

        public Notification Get(int id)
        {
            return _context.Notifications.Find(id);
        }

        public List<Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void Update(Notification notification)
        {
            _context.Notifications.Update(notification);
            _context.SaveChanges();
        }

        public void Delete(Notification notification)
        {
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
        }
    }
}
