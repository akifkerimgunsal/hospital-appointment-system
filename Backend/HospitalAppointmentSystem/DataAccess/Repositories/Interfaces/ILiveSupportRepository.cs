using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface ILiveSupportRepository : IBaseRepository<LiveSupport>
    {
        Task<List<LiveSupport>> GetSessionsByUserIdAsync(Guid userId);
    }
}
