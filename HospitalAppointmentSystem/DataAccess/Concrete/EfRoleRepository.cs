using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfRoleRepository : IRoleRepository
    {
        private readonly HospitalContext _context;

        public EfRoleRepository(HospitalContext context)
        {
            _context = context;
        }

        public Role Get(int id)
        {
            return _context.Roles.Find(id);
        }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
