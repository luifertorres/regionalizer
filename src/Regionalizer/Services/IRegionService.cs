using System.Collections.Generic;
using System.Threading.Tasks;
using Regionalizer.Entities;

namespace Regionalizer.Services
{
    public interface IRegionService
    {
        Task<Region> Add(Region region);
        Task<Region> Get(int id);
        Task<IEnumerable<Region>> GetAll();
        Task<Region> Remove(int id);
        Task Update(Region region);
        Task<IEnumerable<Municipality>> GetAllMunicipalities();
        Task AddMunicipalityToRegion(Region region, int municipalityId);
    }
}