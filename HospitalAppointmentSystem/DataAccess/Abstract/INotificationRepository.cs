using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface INotificationRepository
    {
        Notification Get(int id);
        List<Notification> GetAll();
        void Add(Notification notification);
        void Update(Notification notification);
        void Delete(Notification notification);
    }
}
