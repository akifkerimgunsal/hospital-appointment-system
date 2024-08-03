using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfPasswordChangeRepository : IPasswordChangeRepository
    {
        private readonly HospitalContext _context;

        public EfPasswordChangeRepository(HospitalContext context)
        {
            _context = context;
        }

        public PasswordChange Get(int id)
        {
            return _context.PasswordChanges.Find(id);
        }

        public List<PasswordChange> GetAll()
        {
            return _context.PasswordChanges.ToList();
        }

        public void Add(PasswordChange passwordChange)
        {
            _context.PasswordChanges.Add(passwordChange);
            _context.SaveChanges();
        }

        public void Update(PasswordChange passwordChange)
        {
            _context.PasswordChanges.Update(passwordChange);
            _context.SaveChanges();
        }

        public void Delete(PasswordChange passwordChange)
        {
            _context.PasswordChanges.Remove(passwordChange);
            _context.SaveChanges();
        }
    }
}
