using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PasswordChange
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime ChangeDate { get; set; }
        public string NewPasswordHash { get; set; }

        public User User { get; set; }
    }
}
