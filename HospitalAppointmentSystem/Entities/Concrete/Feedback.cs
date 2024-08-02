using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Feedback
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime FeedbackDate { get; set; }
        public string FeedbackText { get; set; }

        public User User { get; set; }
    }
}
