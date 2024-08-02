using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Report
    {
        public int ID { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportData { get; set; }
    }
}
