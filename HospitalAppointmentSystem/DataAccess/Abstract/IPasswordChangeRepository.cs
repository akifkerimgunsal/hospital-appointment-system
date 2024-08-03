using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IPasswordChangeRepository
    {
        PasswordChange Get(int id);
        List<PasswordChange> GetAll();
        void Add(PasswordChange passwordChange);
        void Update(PasswordChange passwordChange);
        void Delete(PasswordChange passwordChange);
    }
}
