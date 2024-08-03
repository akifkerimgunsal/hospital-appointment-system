using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfFeedbackRepository : IFeedbackRepository
    {
        private readonly HospitalContext _context;

        public EfFeedbackRepository(HospitalContext context)
        {
            _context = context;
        }

        public Feedback Get(int id)
        {
            return _context.Feedbacks.Find(id);
        }

        public List<Feedback> GetAll()
        {
            return _context.Feedbacks.ToList();
        }

        public void Add(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public void Update(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            _context.SaveChanges();
        }

        public void Delete(Feedback feedback)
        {
            _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();
        }
    }
}
