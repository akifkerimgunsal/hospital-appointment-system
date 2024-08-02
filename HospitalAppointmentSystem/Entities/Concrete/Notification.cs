using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Notification
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Message { get; set; }

        public User User { get; set; }
    }
}
