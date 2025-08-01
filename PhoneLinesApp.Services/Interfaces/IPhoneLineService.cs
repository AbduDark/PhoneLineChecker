using PhoneLinesApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneLinesApp.Services.Interfaces
{
    public interface IPhoneLineService
    {
        Task<IEnumerable<PhoneLine>> GetAllLinesAsync(string statusFilter, string searchTerm = "");
        Task<PhoneLine> GetLineAsync(int id);
        Task<bool> SaveLineAsync(PhoneLine line);
        Task<bool> DeleteLineAsync(int id);
    }
}
