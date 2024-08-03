using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfUserRoleRepository : IUserRoleRepository
    {
        private readonly HospitalContext _context;

        public EfUserRoleRepository(HospitalContext context)
        {
            _context = context;
        }

        public UserRole Get(int id)
        {
            return _context.UserRoles.Find(id);
        }

        public List<UserRole> GetAll()
        {
            return _context.UserRoles.ToList();
        }

        public void Add(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public void Update(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            _context.SaveChanges();
        }

        public void Delete(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }
    }
}
