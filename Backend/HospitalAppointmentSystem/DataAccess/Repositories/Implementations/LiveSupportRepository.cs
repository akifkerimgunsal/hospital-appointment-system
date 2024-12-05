using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class LiveSupportRepository : BaseRepository<LiveSupport>, ILiveSupportRepository
    {
        private readonly AppDbContext _context;

        public LiveSupportRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Belirli bir kullanıcıya ait canlı destek oturumlarını getir
        public async Task<List<LiveSupport>> GetSessionsByUserIdAsync(Guid userId)
        {
            return await _context.LiveSupports
                                 .Where(l => l.UserId == userId)
                                 .ToListAsync();
        }
    }


}
