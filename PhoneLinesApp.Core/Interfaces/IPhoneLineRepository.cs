using PhoneLinesApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneLinesApp.Core.Interfaces
{
    public interface IPhoneLineRepository
    {
        Task<IEnumerable<PhoneLine>> GetAllAsync();
        Task<PhoneLine> GetByIdAsync(int id);
        Task AddAsync(PhoneLine line);
        void Update(PhoneLine line);
        void Remove(PhoneLine line);
        Task<int> SaveChangesAsync();
    }
}
