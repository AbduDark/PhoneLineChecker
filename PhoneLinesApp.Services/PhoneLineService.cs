using PhoneLinesApp.Core.Interfaces;
using PhoneLinesApp.Core.Models;
using PhoneLinesApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneLinesApp.Services
{
    public class PhoneLineService : IPhoneLineService
    {
        private readonly IPhoneLineRepository _repo;
        public PhoneLineService(IPhoneLineRepository repo) => _repo = repo;

        public async Task<IEnumerable<PhoneLine>> GetAllLinesAsync(string status, string search)
        {
            var all = await _repo.GetAllAsync();
            var query = all.AsQueryable();

            if (status == "Active") query = query.Where(l => l.IsActive);
            if (status == "Inactive") query = query.Where(l => !l.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(l =>
                    l.PhoneNumber.Contains(search) ||
                    (l.Notes ?? "").Contains(search));
            }

            return query.OrderBy(l => l.PhoneNumber);
        }

        public Task<PhoneLine> GetLineAsync(int id)
            => _repo.GetByIdAsync(id);

        public async Task<bool> SaveLineAsync(PhoneLine line)
        {
            if (line.Id == 0)
                await _repo.AddAsync(line);
            else
                _repo.Update(line);

            return await _repo.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLineAsync(int id)
        {
            var line = await _repo.GetByIdAsync(id);
            if (line == null) return false;
            _repo.Remove(line);
            return await _repo.SaveChangesAsync() > 0;
        }
    }
}
