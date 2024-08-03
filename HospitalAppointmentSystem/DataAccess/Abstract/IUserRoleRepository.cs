using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserRoleRepository
    {
        UserRole Get(int id);
        List<UserRole> GetAll();
        void Add(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(UserRole userRole);
    }
}
