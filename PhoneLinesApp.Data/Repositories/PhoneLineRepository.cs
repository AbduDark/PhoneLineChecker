using PhoneLinesApp.Core.Interfaces;
using PhoneLinesApp.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PhoneLinesApp.Data.Repositories
{
    public class PhoneLineRepository : IPhoneLineRepository
    {
        private readonly ApplicationDbContext _ctx;
        public PhoneLineRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<PhoneLine>> GetAllAsync()
            => await _ctx.PhoneLines.ToListAsync();

        public async Task<PhoneLine> GetByIdAsync(int id)
            => await _ctx.PhoneLines.FindAsync(id);

        public async Task AddAsync(PhoneLine line)
            => _ctx.PhoneLines.Add(line);

        public void Update(PhoneLine line)
            => _ctx.Entry(line).State = EntityState.Modified;

        public void Remove(PhoneLine line)
            => _ctx.PhoneLines.Remove(line);

        public async Task<int> SaveChangesAsync()
            => await _ctx.SaveChangesAsync();
    }
}
