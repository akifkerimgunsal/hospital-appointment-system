using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IFeedbackRepository
    {
        Feedback Get(int id);
        List<Feedback> GetAll();
        void Add(Feedback feedback);
        void Update(Feedback feedback);
        void Delete(Feedback feedback);
    }
}
