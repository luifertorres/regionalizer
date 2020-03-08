using System.Collections.Generic;
using System.Threading.Tasks;
using Regionalizer.Entities;

namespace Regionalizer.Services
{
    public interface IMunicipalityService
    {
        Task<Municipality> Add(Municipality municipality);
        Task<Municipality> Get(int id);
        Task<IEnumerable<Municipality>> GetAll();
        Task<Municipality> Remove(int id);
        Task Update(Municipality municipality);
    }
}